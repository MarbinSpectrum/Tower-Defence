using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TurnState { Shop,Defence};

public class TurnManager : Singleton<TurnManager>
{
    public bool gameStart = false;
    public TurnState nowTurnState = TurnState.Defence;
    public int turnNum = 0;

    public void Update()
    {
        if (!gameStart)
            return;

        if(nowTurnState == TurnState.Defence)
        {
            if(!MonsterSpwan.MonsterExist())
            {
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
            }
        }
    }
}
