using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class MonsterSpwan : Singleton<MonsterSpwan>
{
    //단계별 몬스터를 관리하는 리스트
    [TableList(DrawScrollView = true)]
    public List<MonsterGroup> monsterList = new List<MonsterGroup>();

    //몬스터를 관리하는 리스트
    [NonSerialized]
    public List<MonsterObect> monsters = new List<MonsterObect>();

    //몬스터 생성
    public static void SpawnMonster(int level) => Instance.pSpawnMonster(level);

    private void pSpawnMonster(int level) => StartCoroutine(Cor_SpawnMonster(level));

    IEnumerator Cor_SpawnMonster(int level)
    {
        int mN = 0;
        for (int i = 0; i < monsterList[level].monsters.Count; i++)
            mN += monsterList[level].monsters[i].spawnNum;
        existMonster = mN;
        yield return new WaitWhile(() => { return Timer.time > 0; });
        monsters.Clear();

        for (int j = 0; j < monsterList[level].monsters.Count; j++)
            for (int i = 0; i < monsterList[level].monsters[j].spawnNum; i++)
            {
                Vector3 startPos = CreateMap.SpawnPos;
                GameObject temp = Instantiate(monsterList[level].monsters[j].ojbect, startPos, Quaternion.identity);
                temp.transform.LookAt(CreateMap.GetPosVector(CreateMap.NextMovePos(CreateMap.DESTINATION)));
                MonsterObect mO = temp.GetComponent<MonsterObect>();
                mO.hp = monsterList[level].monsters[j].hp;
                mO.maxhp = monsterList[level].monsters[j].hp;
                mO.speed = monsterList[level].monsters[j].speed;
                monsters.Add(mO);

                yield return new WaitForSeconds(1f);
            }
    }

    public static void Init()
    {
        existMonster = 0;
        Instance.monsters.Clear();
    }
    public static int existMonster = 0;

    //몬스터 존재여부를 검사
    public static bool MonsterExist()
    {
        if (existMonster > 0)
            return true;

        for (int i = 0; i < Instance.monsters.Count; i++)
            if (Instance.monsters[i] != null)
                return true;
        return false;
    }

}
