using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerState : OpenUI<PlayerState>
{
    public int level;
    public int hp;
    [NonSerialized]
    public int maxHP;
    public int gold;

    public int maxTower;
    public int nowTower;

    public int power;

    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI hpUI;
    public TextMeshProUGUI towerUI;

    [NonSerialized]
    public bool gameOver = false;

    public static void Init()
    {
        Instance.gameOver = false;
        Instance.level = 0;
        Instance.hp = 100;
        Instance.gold = 0;
        Instance.maxTower = 0;
        Instance.nowTower = 0;
        Instance.power = 0;
    }

    public Image hpBar;
    private void Update()
    {
        goldUI.text = gold.ToString();
        hpUI.text = hp.ToString() + "/" + maxHP;
        towerUI.text = nowTower + "/" + maxTower;
        hpBar.fillAmount = (float)hp / (float)maxHP;
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

        if (hp <= 0 && !gameOver)
        {
            gameOver = true;
            SoundManager.StopBGM();
            GameOverScreen.Instacne.GameOver();
        }
    }

    public static void SetPlayerData(int lv,int hp,int g,int mT)
    {
        Instance.level = lv;
        Instance.hp = hp;
        Instance.maxHP = hp;
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
