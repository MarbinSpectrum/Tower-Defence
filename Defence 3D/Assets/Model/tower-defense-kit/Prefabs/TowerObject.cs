using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

public class TowerObject : MonoBehaviour
{
    public float coolTime = 0;
    private float delay = 0;
    public int damage = 1;
    public float range = 2;
    public int critical = 0;

    public TowerResource towerResource;

    public int towerLevel;

    public GameObject head;

    public List<TowerLevelMat> towerLevelMats;

    private Vector3 lookVec;

    private void Update()
    {
        TowerState();
        RedTowerBuff();
        Attack();
        UpdateMats();
    }

    private void TowerState()
    {
        if (towerResource != null)
        {
            if (Exception.IndexOutRange(towerLevel, towerResource.coolTime))
                coolTime = towerResource.coolTime[towerLevel];
            if (Exception.IndexOutRange(towerLevel, towerResource.damage))
                damage = towerResource.damage[towerLevel];
            if (Exception.IndexOutRange(towerLevel, towerResource.range))
                range = towerResource.range[towerLevel];
            if (Exception.IndexOutRange(towerLevel, towerResource.critical))
                critical = towerResource.critical[towerLevel];

            float speedB = 1;
            float powB = 1;
            int criticalB = 0;

            for (int i = 0; i < TowerManager.Instance.towerObj.Count; i++)
            {
                if (TowerManager.Instance.towerObj[i] == null)
                    continue;
                if (!TowerManager.Instance.towerObj[i].towerResource.buffTower)
                    continue;
                float dis = TowerManager.Instance.towerObj[i].range;
                Vector3 a = TowerManager.Instance.towerObj[i].transform.position;
                Vector3 b = transform.position;
                if (Vector3.Distance(a, b) > dis)
                    continue;
                int buffTowerLevel = TowerManager.Instance.towerObj[i].towerLevel;
                for (int j = 0; j < TowerManager.Instance.towerObj[i].towerResource.buffs.Count; j++)
                {
                    if (TowerManager.Instance.towerObj[i].towerResource.buffs[j].buffType == Buff.Pow)
                        powB = Mathf.Max(powB, TowerManager.Instance.towerObj[i].towerResource.buffs[j].value[buffTowerLevel]);
                    else if (TowerManager.Instance.towerObj[i].towerResource.buffs[j].buffType == Buff.Speed)
                        speedB = Mathf.Max(speedB, TowerManager.Instance.towerObj[i].towerResource.buffs[j].value[buffTowerLevel]);
                    else if (TowerManager.Instance.towerObj[i].towerResource.buffs[j].buffType == Buff.Critical)
                        criticalB = Mathf.Max(criticalB, (int)TowerManager.Instance.towerObj[i].towerResource.buffs[j].value[buffTowerLevel]);
                }
            }

            damage = (int)((float)damage + powB);
            coolTime /= speedB;
            coolTime = Mathf.Max(coolTime, 0.001f);
            critical += criticalB;

        }
    }

    private void Attack()
    {
        if (towerResource == null)
            return;
        if (towerResource.buffTower)
            return;

        //공격처리
        delay -= Time.deltaTime;
        for (int i = 0; i < MonsterSpwan.Instance.monsters.Count; i++)
            if (MonsterSpwan.Instance.monsters[i] != null && Vector3.Distance(transform.position, MonsterSpwan.Instance.monsters[i].transform.position) <= range && delay <= 0)
            {
                Vector3 temp = MonsterSpwan.Instance.monsters[i].transform.position - transform.position;
                temp = Quaternion.LookRotation(temp).eulerAngles;
                lookVec = new Vector3(0, temp.y, 0);
                delay = coolTime;
                int nowDamage = damage + PlayerState.Instance.power;
                if(towerResource.name != "독 타워")
                    nowDamage += MonsterSpwan.Instance.monsters[i].posionDamage;
                int random = Random.Range(0, 100);
                if (random < critical)
                    nowDamage *= 2;
                MonsterSpwan.Instance.monsters[i].hp -= nowDamage;
                MonsterSpwan.Instance.monsters[i].GetDebuff(towerResource.debuffs, towerLevel);
                break;
            }

        //머리 회전
        head.transform.rotation = Quaternion.Lerp(head.transform.rotation, Quaternion.Euler(lookVec), Time.deltaTime * 5);
    }

    private void UpdateMats()
    {
        foreach (TowerLevelMat m in towerLevelMats)
            m.level = towerLevel;
    }

    float levelUpTime = 0;
    int levelUpBuff = 0;
    private void RedTowerBuff()
    {
        if (towerResource == null)
            return;
        if (!towerResource.levelUpTower)
            return;
        if (TurnManager.Instance.nowTurnState == TurnState.Shop)
        {
            levelUpBuff = 0;
            return;
        }
        levelUpTime += Time.deltaTime;
        if (levelUpTime < 1)
            return;
        levelUpTime = 0;
        levelUpBuff += towerResource.levelUpTowerBuff[towerLevel];
        damage += levelUpBuff;
    }
}
