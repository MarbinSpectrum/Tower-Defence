using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerState : OpenUI<PlayerState>
{
    public int level;
    public int hp;
    public int gold;

    public int maxTower;
    public int nowTower;

    public int power;

    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI hpUI;
    public TextMeshProUGUI towerUI;

    public Image hpBar;
    private void Update()
    {
        goldUI.text = gold.ToString();
        hpUI.text = hp.ToString() + "/100";
        towerUI.text = nowTower + "/" + maxTower;
        hpBar.fillAmount = (float)hp / (float)100;
        if (!CameraManager.nowZoom && animator.GetBool("Open"))
            SetUIState(false);
        else if (CameraManager.nowZoom && !animator.GetBool("Open"))
            SetUIState(true);

        {
            int bG = (Instance.level + 1) * 6;
            int iG = (Instance.gold) / 3;
            int rG = bG + iG;
            baseGold.text = "+" + bG;
            interestGold.text = "+" + iG;
            resultGold.text = rG.ToString();
        }
    }

    public static void SetPlayerData(int lv,int hp,int g,int mT)
    {
        Instance.level = lv;
        Instance.hp = hp;
        Instance.gold = g;
        Instance.maxTower = mT;
        Instance.nowTower = 0;
    }

    public static void GetMoney()
    {
        Instance.gold += (Instance.gold) / 3;
        Instance.gold += (Instance.level + 1) * 6;
    }

    public TextMeshProUGUI baseGold;
    public TextMeshProUGUI interestGold;
    public TextMeshProUGUI resultGold;

}
