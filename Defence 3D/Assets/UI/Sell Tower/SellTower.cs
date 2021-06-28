using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SellTower : OpenUI<SellTower>
{
    public static int nowPr = 1;

    public TextMeshProUGUI text;

    public bool isZone = false;

    public void InSellZone(bool b) => isZone = b;

    public void Update()
    {
        text.text = "+" + nowPr;

    }



}
