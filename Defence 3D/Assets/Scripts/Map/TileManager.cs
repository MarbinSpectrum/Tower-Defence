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

        if (tile.towerExist)
            return TowerCheckState.Tower_Exist;

        if (PlayerState.Instance.gold < cost)
            return TowerCheckState.Gold_Lack;

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
            ShowText.ViewText("��ȭ�� �����մϴ�.");
            return;
        }

        if (checkS == TowerCheckState.Tower_Exist)
        {
            ShowText.ViewText("�ش� ��ġ�� �̹� Ÿ���� �����մϴ�.");
            return;
        }

        if (checkS == TowerCheckState.Tower_MAX)
        {
            ShowText.ViewText("Ÿ���� �ִ� ������ �ʰ��߽��ϴ�.");
            return;
        }

        if (checkS != TowerCheckState.Can_Set)
            return;

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

        PlayerState.Instance.nowTower++;

        TowerManager.ReplaceTowerList();
    }
}
