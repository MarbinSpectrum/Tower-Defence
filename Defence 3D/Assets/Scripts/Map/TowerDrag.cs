using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

public class TowerDrag : MonoBehaviour
{
    public static TowerDrag nowDrag;

    [NonSerialized]
    public Vector2Int firstPos;

    [NonSerialized]
    public bool drag = false;

    private TowerObject towerObject;
    private Collider colider;
    private static bool mouseUp = true;

    private void Awake()
    {
        if (towerObject == null)
            towerObject = GetComponent<TowerObject>();
        if (colider == null)
            colider = GetComponent<Collider>();
    }

    public static void CancleDrag()
    {
        if (nowDrag == null)
            return;

        SellTower.SetUIState(false);
        SellTower.Instance.InSellZone(false);

        MouseManager.isDragging = false;
        CameraManager.ChangeZoom(CameraManager.ZOOM_IN);

        nowDrag.SetFirstPos();
        TileManager.Instance.tile[nowDrag.firstPos.x, nowDrag.firstPos.y].tower = nowDrag.gameObject;
        nowDrag.towerObject.enabled = true;
        nowDrag.colider.enabled = true;
        nowDrag.drag = false;
        nowDrag = null;

    }

    private void OnMouseDrag()
    {
        if(nowDrag == null && mouseUp && TurnManager.Instance.nowTurnState == TurnState.Shop)
        {
            SellTower.nowPr = (1 << towerObject.towerLevel) * (towerObject.towerResource.level + 1);

            nowDrag = this;
            drag = true;
            mouseUp = false;

            SellTower.SetUIState(true);

            TileManager.Instance.tile[firstPos.x, firstPos.y].tower = null;

            towerObject.enabled = false;

            MouseManager.isDragging = true;
            CameraManager.ChangeZoom(CameraManager.ZOOM_OUT);
        }
    }

    private void Update()
    {
        if (drag)
            transform.position = MouseManager.mousePos;
    }

    private void OnMouseUp()
    {
        DropTower();
        mouseUp = true;
    }

    public void DropTower()
    {
        if (!drag)
            return;
        TowerCheckState checkState = TileManager.CheckTowerSet(0, true);
        if (checkState == TowerCheckState.Can_Set)
        {
            transform.position = new Vector3(MouseManager.nowTile.x, 0.2f, MouseManager.nowTile.y);
            TileManager.Instance.tile[MouseManager.nowTile.x, MouseManager.nowTile.y].tower = gameObject;
            firstPos = new Vector2Int(MouseManager.nowTile.x, MouseManager.nowTile.y);
        }
        else if (checkState == TowerCheckState.Tower_Exist)
        {
            if (TowerSameCheck(TileManager.Instance.tile[MouseManager.nowTile.x, MouseManager.nowTile.y].tower))
            {
                Destroy(TileManager.Instance.tile[MouseManager.nowTile.x, MouseManager.nowTile.y].tower);

                towerObject.towerLevel++;
                transform.position = new Vector3(MouseManager.nowTile.x, 0.2f, MouseManager.nowTile.y);
                TileManager.Instance.tile[MouseManager.nowTile.x, MouseManager.nowTile.y].tower = gameObject;
                firstPos = new Vector2Int(MouseManager.nowTile.x, MouseManager.nowTile.y);

                TowerManager.ReplaceTowerList();
                PlayerState.Instance.nowTower--;
            }
            else
            {
                Vector2Int posTemp = firstPos;

                TileManager.Instance.tile[firstPos.x, firstPos.y].tower = TileManager.Instance.tile[MouseManager.nowTile.x, MouseManager.nowTile.y].tower;
                TileManager.Instance.tile[MouseManager.nowTile.x, MouseManager.nowTile.y].tower = gameObject;

                TowerDrag tempDrag = TileManager.Instance.tile[posTemp.x, posTemp.y].tower.GetComponent<TowerDrag>();

                firstPos = new Vector2Int(MouseManager.nowTile.x, MouseManager.nowTile.y);
                tempDrag.firstPos = posTemp;

                SetFirstPos();
                tempDrag.SetFirstPos();
            }
        }
        else
        {
            SetFirstPos();
            TileManager.Instance.tile[firstPos.x, firstPos.y].tower = gameObject;
        }

        SellTower.SetUIState(false);

        nowDrag = null;
        drag = false;

        towerObject.enabled = true;

        MouseManager.isDragging = false;
        CameraManager.ChangeZoom(CameraManager.ZOOM_IN);

        if (SellTower.Instance.isZone)
        {
            SellTower.Instance.isZone = false;
            Destroy(gameObject);
            TowerManager.ReplaceTowerList();
            PlayerState.Instance.gold += (1 << towerObject.towerLevel) * (towerObject.towerResource.level + 1);
            PlayerState.Instance.nowTower--;
        }
    }

    public bool TowerSameCheck(GameObject b)
    {
        TowerObject bt = b.GetComponent<TowerObject>();
        return (towerObject.towerLevel != 3 && towerObject.towerLevel == bt.towerLevel && towerObject.towerResource.name == bt.towerResource.name);
    }

    public void SetFirstPos() => transform.position = new Vector3(firstPos.x, 0.2f, firstPos.y);
}
