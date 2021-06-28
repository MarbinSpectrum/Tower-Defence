using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class MonsterSpwan : Singleton<MonsterSpwan>
{
    //단계별 몬스터를 관리하는 리스트
    [TableList(DrawScrollView = true)]
    public List<Monster> monsterList = new List<Monster>();

    //몬스터를 관리하는 리스트
    [NonSerialized]
    public List<MonsterObect> monsters = new List<MonsterObect>();

    //몬스터 생성
    public static void SpawnMonster(int level) => Instance.pSpawnMonster(level);

    private void pSpawnMonster(int level) => StartCoroutine(Cor_SpawnMonster(level));

    IEnumerator Cor_SpawnMonster(int level)
    {
        existMonster = monsterList[level].spawnNum;
        yield return new WaitWhile(() => { return Timer.time > 0; });
        monsters.Clear();
        for (int i = 0; i < monsterList[level].spawnNum; i++)
        {
            Vector3 startPos = CreateMap.SpawnPos;
            GameObject temp = Instantiate(monsterList[level].ojbect, startPos, Quaternion.identity);
            temp.transform.LookAt(CreateMap.GetPosVector(CreateMap.NextMovePos(CreateMap.DESTINATION)));
            MonsterObect mO = temp.GetComponent<MonsterObect>();
            mO.hp  = monsterList[level].hp;
            mO.maxhp = monsterList[level].hp;
            mO.speed = monsterList[level].speed;
            monsters.Add(mO);

            yield return new WaitForSeconds(1f);
        }
    }

    public static int existMonster = 0;
    //몬스터 존재여부를 검사
    public static bool MonsterExist()
    {
        return existMonster > 0;
    }

}
