using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TurnState { Shop,Defence};

public class TurnManager : Singleton<TurnManager>
{
    public bool gameStart = false;
    public TurnState nowTurnState = TurnState.Defence;
    public int turnNum = 0;

    public static void Init()
    {
        Instance.gameStart = false;
        Instance.turnNum = 0;
        Instance.nowTurnState = TurnState.Defence;
    }

    public void Update()
    {
        if (!gameStart)
            return;

        if (PlayerState.Instance.gameOver)
            return;


        if(nowTurnState == TurnState.Defence)
        {
            if(!MonsterSpwan.MonsterExist())
            {
                SoundManager.PlayBGM(BGM.Soldiers);
                nowTurnState = TurnState.Shop;
                Timer.time = Mathf.Min(60, 20 + turnNum * 5);
                Shop.SetShop();
                Shop.SetUIState(true);
                Upgrade.SetUIState(true);
                MonsterSpwan.SpawnMonster(turnNum);
                PlayerState.GetMoney();
                turnNum++;
            }
        }
        else
        {
            if(Timer.time <= 0)
            {
                nowTurnState = TurnState.Defence;
                Shop.SetUIState(false);
                Upgrade.SetUIState(false);
                Drag.CancleDrag();
                TowerDrag.CancleDrag();
                if (turnNum % 5 == 0)
                    SoundManager.PlayBGM(BGM.Boss);
                else
                    SoundManager.PlayBGM(BGM.Soldiers);
            }
        }
    }
}
