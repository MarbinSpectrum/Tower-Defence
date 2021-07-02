using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

public enum TowerCheckState { IndexOut, Tile_NULL , Tower_Exist, Gold_Lack,Tower_MAX, Can_Set };

public class TileManager : Singleton<TileManager>
{
    public Tile[,] tile = new Tile[(CreateMap.C + 1) * CreateMap.DIS + 1, (CreateMap.R + 1) * CreateMap.DIS + 1];

    public static TowerCheckState CheckTowerSet(int cost,bool towerDrag = false)
    {
        if (!Exception.IndexOutRange(MouseManager.nowTile.x, MouseManager.nowTile.y, Instance.tile))
            return TowerCheckState.IndexOut;

        Tile tile = TileManager.Instance.tile[MouseManager.nowTile.x, MouseManager.nowTile.y];

        if (tile == null)
            return TowerCheckState.Tile_NULL;

        if (PlayerState.Instance.gold < cost)
            return TowerCheckState.Gold_Lack;

        if (tile.towerExist)
            return TowerCheckState.Tower_Exist;


        if (PlayerState.Instance.nowTower >= PlayerState.Instance.maxTower && !towerDrag)
            return TowerCheckState.Tower_MAX;

        return TowerCheckState.Can_Set;
    }

    public static void TowerSet(GameObject tower, TowerSlot towerSlot)
    {
        int cost = towerSlot.towerResource.level + 1;
        TowerCheckState checkS = CheckTowerSet(cost);

        if(checkS == TowerCheckState.Gold_Lack)
        {
            ShowText.ViewText("금화가 부족합니다.");
            SoundManager.PlaySE(SE.Error);
            return;
        }

        if (checkS == TowerCheckState.Tower_Exist)
        {
            GameObject a = tower;
            GameObject b = Instance.tile[MouseManager.nowTile.x, MouseManager.nowTile.y].tower;

            if(TowerDrag.TowerSameCheck(a,b))
            {
                towerSlot.hide = true;
                PlayerState.Instance.gold -= cost;

                TowerObject tO = b.GetComponent<TowerObject>();
                tO.towerLevel++;

                SoundManager.PlaySE(SE.Gold);
                SoundManager.PlaySE(SE.LevelUp);
                CameraManager.VibrationCamera(0.2f);

                TowerManager.ReplaceTowerList();

                return;
            }
            else
            {
                ShowText.ViewText("해당 위치에 이미 타워가 존재합니다.");
                SoundManager.PlaySE(SE.Error);
                return;
            }
        }

        if (checkS != TowerCheckState.Can_Set)
            return;

        if (checkS == TowerCheckState.Tower_MAX)
        {
            ShowText.ViewText("타워의 최대 갯수를 초과했습니다.");
            SoundManager.PlaySE(SE.Error);
            return;
        }

        towerSlot.hide = true;
        PlayerState.Instance.gold -= cost;
        GameObject temp = Instantiate(tower, new Vector3(MouseManager.nowTile.x, 0.2f, MouseManager.nowTile.y), Quaternion.identity);
        Tile tile = Instance.tile[MouseManager.nowTile.x, MouseManager.nowTile.y];
        tile.tower = temp;

        if (temp.GetComponent<TowerDrag>() == null)
            temp.AddComponent<TowerDrag>();
        temp.GetComponent<TowerDrag>().firstPos = new Vector2Int(MouseManager.nowTile.x, MouseManager.nowTile.y);
        if (temp.GetComponent<TowerObject>() == null)
            temp.AddComponent<TowerObject>();
        temp.GetComponent<TowerObject>().towerResource = towerSlot.towerResource;
        SoundManager.PlaySE(SE.Gold);
        SoundManager.PlaySE(SE.ObjMove);

        PlayerState.Instance.nowTower++;

        TowerManager.ReplaceTowerList();
    }
}
