using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    void Start()
    {
        CreateMap.Create();
        PlayerState.SetPlayerData(0, 100, 0, 3);
        Shop.SetShop();
        MonsterSpwan.existMonster = 0;
        TurnManager.Instance.gameStart = true;
    }
}
