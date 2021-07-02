using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    public int startHP = 30;
    public int startTower = 3;
    public int startGold = 0;
    public int startLevel = 0;
    public BGM startBGM;

    [Range(0.5f,3f)]
    public float timeS = 1;
    void Start()
    {

        Time.timeScale = timeS;

        CreateMap.Init();
        CreateMap.Create();

        PlayerState.Init();
        PlayerState.SetPlayerData(startLevel, startHP, startGold, startTower);

        Shop.SetShop();

        MonsterSpwan.Init();
        MonsterSpwan.existMonster = 0;

        TurnManager.Init();
        TurnManager.Instance.gameStart = true;

        SoundManager.PlayBGM(startBGM);

    }
}
