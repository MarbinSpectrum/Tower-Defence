using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerManager : Singleton<TowerManager>
{
    [NonSerialized]
    public List<TowerObject> towerObj = new List<TowerObject>();

    public static void ReplaceTowerList(GameObject remove = null)
    {
        Instance.towerObj.Clear();
        for (int x = 0; x < (CreateMap.C + 1) * CreateMap.DIS + 1; x++)
            for (int y = 0; y < (CreateMap.R + 1) * CreateMap.DIS + 1; y++)
                if (TileManager.Instance.tile[x, y] != null && TileManager.Instance.tile[x, y].tower != null)
                    if(remove != TileManager.Instance.tile[x, y].tower)
                        Instance.towerObj.Add(TileManager.Instance.tile[x, y].tower.GetComponent<TowerObject>());

        Instance.towerObj.Sort((TowerObject a, TowerObject b) => 
        a.towerResource.buffTower && !b.towerResource.buffTower ? -1 :
         !a.towerResource.buffTower && b.towerResource.buffTower ? +1 : 0);

        //string s = "";
        //for (int i = 0; i < Instance.towerObj.Count; i++)
        //    s += Instance.towerObj[i].transform.name + " , ";
        //    Debug.Log(s);

        for (int i = 0; i < Instance.towerObj.Count; i++)
            Instance.towerObj[i].TowerState();

    }
}
