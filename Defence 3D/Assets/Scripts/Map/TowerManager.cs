using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerManager : Singleton<TowerManager>
{
    [NonSerialized]
    public List<TowerObject> towerObj = new List<TowerObject>();

    public static void ReplaceTowerList()
    {
        Instance.towerObj.Clear();
        for (int x = 0; x < (CreateMap.C + 1) * CreateMap.DIS + 1; x++)
            for (int y = 0; y < (CreateMap.R + 1) * CreateMap.DIS + 1; y++)
                if (TileManager.Instance.tile[x, y] != null && TileManager.Instance.tile[x, y].tower != null)
                    Instance.towerObj.Add(TileManager.Instance.tile[x, y].tower.GetComponent<TowerObject>());
    }
}
