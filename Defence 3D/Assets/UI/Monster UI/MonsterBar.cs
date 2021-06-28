using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBar : MonoBehaviour
{
    public Image bar;

    public int maxHP;
    public int nowHP;

    public void Update()
    {
        bar.fillAmount = ((float)nowHP / (float)maxHP);
    }
}
