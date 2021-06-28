using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TowerSlot : MonoBehaviour
{
    [NonSerialized]
    public TowerResource towerResource;

    public GameObject item;
    public bool hide;

    public Image window0;
    public Image window1;
    public Image img;
    public TextMeshProUGUI price;

    public TextMeshProUGUI explain;

    public void SetResource(TowerResource tower)
    {
        towerResource = tower;
        window0.sprite = Shop.Instance.levelSprite[tower.level];
        window1.color = Shop.Instance.levelColor[tower.level];
        img.sprite = tower.img;
        price.text = (tower.level + 1).ToString();
        explain.text = tower.explain;
    }

    public void Update()
    {
        item.SetActive(!hide);
    }
}
