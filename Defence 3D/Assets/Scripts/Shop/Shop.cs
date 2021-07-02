using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Sirenix.OdinInspector;

public class Shop : OpenUI<Shop>
{
    private TowerResource []towerData;

    [NonSerialized]
    public List<TowerResource> shopTower = new List<TowerResource>();

    [TableList(DrawScrollView = true)]
    public List<LevelPercent> levelPercents = new List<LevelPercent>();
    public List<Color> levelColor = new List<Color>();
    public List<Sprite> levelSprite = new List<Sprite>();
    public List<TowerSlot> towerSlot = new List<TowerSlot>();

    private void Start()
    {
        towerData = Resources.LoadAll<TowerResource>("Data");
    }

    const int REFRESH_VALUE = 2;

    public void RefreshShop()
    {
        if (PlayerState.Instance.gold < REFRESH_VALUE)
        {
            SoundManager.PlaySE(SE.Error);
            return;
        }
        SoundManager.PlaySE(SE.Gold);
        PlayerState.Instance.gold -= REFRESH_VALUE;
        SetShop();
    }

    public static void SetShop()
    {
        Instance.pSetShop();
    }

    private void pSetShop()
    {
        shopTower.Clear();

        for (int j = 0; j < 5; j++)
        {
            int sum = 0;
            List<int> LV = new List<int>();
            LV.Add(levelPercents[PlayerState.Instance.level].lv0);
            LV.Add(levelPercents[PlayerState.Instance.level].lv1);
            LV.Add(levelPercents[PlayerState.Instance.level].lv2);
            LV.Add(levelPercents[PlayerState.Instance.level].lv3);

            for (int i = 0; i < LV.Count; i++)
                sum += LV[i];

            int r = UnityEngine.Random.Range(0, sum);

            int s = 0;
            int e = 0;

            for (int i = 0; i < LV.Count; i++)
            {
                e += LV[i];
                if (s <= r && r < e)
                {
                    shopTower.Add(GetRandomTower(i));
                    break;
                }
                s = e;
            }


        }

        for (int i = 0; i < towerSlot.Count; i++)
        {
            towerSlot[i].SetResource(shopTower[i]);
            towerSlot[i].hide = false;
        }
    }

    private TowerResource GetRandomTower(int lv)
    {
        List<TowerResource> temp = new List<TowerResource>();
        for (int i = 0; i < towerData.Length; i++)
            if (towerData[i].level == lv)
                temp.Add(towerData[i]);

        if (temp.Count == 0)
            return GetRandomTower(0);
        int r = UnityEngine.Random.Range(0, temp.Count);
        return temp[r];
    }
}
