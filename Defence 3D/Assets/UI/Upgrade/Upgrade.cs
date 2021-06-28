using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : OpenUI<Upgrade>
{
    int h = 1;
    public void BuyHearth()
    {
        if (PlayerState.Instance.gold < h * h * 2)
            return;
        PlayerState.Instance.gold -= h * h * 2;
        PlayerState.Instance.hp += 5;
        PlayerState.Instance.hp = Mathf.Min(PlayerState.Instance.hp, 100);
        h++;
    }

    int l = 1;
    public void BuyLevel()
    {
        if (l == 9 || PlayerState.Instance.gold < l * l * 2)
            return;
        PlayerState.Instance.gold -= l * l * 2;
        PlayerState.Instance.level += 1;
        PlayerState.Instance.maxTower += 1;
        l++;
    }

    int p = 1;
    public void BuyPower()
    {
        if (p == 20 || PlayerState.Instance.gold < p * p * 2)
            return;
        PlayerState.Instance.gold -= p * p * 2;
        PlayerState.Instance.power += 1;
        p++;
    }

    public TextMeshProUGUI hearthText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI powerText;



    public TextMeshProUGUI levelNow;
    public GameObject maxLevel;
    public TextMeshProUGUI powerNow;
    public GameObject maxPow;

    private void Update()
    {
        hearthText.text = (h * h * 2).ToString();
        if(l < 9)
        {
            levelText.gameObject.SetActive(true);
            maxLevel.SetActive(false);
            levelText.text = (l * l * 2).ToString();
        }
        else
        {
            levelText.gameObject.SetActive(false);
            maxLevel.SetActive(true);
        }
        if (p < 20)
        {
            powerText.gameObject.SetActive(true);
            maxPow.SetActive(false);
            powerText.text = (p * p * 2).ToString();
        }
        else
        {
            powerText.gameObject.SetActive(false);
            maxPow.SetActive(true);
        }

        levelNow.text = "현재 연구 진행도 " + l + "/9";
        powerNow.text = "현재 연구 진행도 " + p + "/20";
    }
}
