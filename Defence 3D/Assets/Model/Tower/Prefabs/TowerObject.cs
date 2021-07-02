using Custom;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    public float coolTime = 0;
    private float delay = 0;
    public int damage = 1;
    public float range = 2;
    public int critical = 0;

    public TowerResource towerResource;

    public Bullet bullet;

    public List<Transform> effectStart;
    public Effect effect;
    public Effect fireEffect;

    public int towerLevel;

    public GameObject head;

    public List<TowerLevelMat> towerLevelMats;
    private Vector3 lookVec;

    [System.NonSerialized]
    public bool buffP = false;
    [System.NonSerialized]
    public bool buffS = false;
    [System.NonSerialized]
    public bool buffC = false;

    public LineRenderer linkObj;
    private List<LineRenderer> linkList = new List<LineRenderer>();
    public List<Material> lineMat = new List<Material>();

    private void Awake()
    {
        foreach (Buff bb in System.Enum.GetValues(typeof(Buff)))
        {
            LineRenderer temp = Instantiate(linkObj);
            temp.transform.SetParent(transform);
            temp.material = lineMat[(int)bb];
            temp.SetPosition(0, transform.position + new Vector3(0, 0.2f, 0));
            linkList.Add(temp);
        }
    }

    private void Update()
    {
        if (PlayerState.Instance.gameOver)
            return;
        LevelUpTowerBuff();
        Attack();
        UpdateMats();
    }

    public void TowerState()
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

            float[] buffList = { 1, 0, 0 };
            Transform[] buffTrans = { null, null, null };

            if (!towerResource.buffTower)
            {
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
                        foreach (Buff bb in System.Enum.GetValues(typeof(Buff)))
                            if (TowerManager.Instance.towerObj[i].towerResource.buffs[j].buffType == bb)
                                if (buffList[(int)bb] < TowerManager.Instance.towerObj[i].towerResource.buffs[j].value[buffTowerLevel])
                                {
                                    buffList[(int)bb] = TowerManager.Instance.towerObj[i].towerResource.buffs[j].value[buffTowerLevel];
                                    buffTrans[(int)bb] = TowerManager.Instance.towerObj[i].transform;
                                    //Debug.Log(TowerManager.Instance.towerObj[i].transform.name);
                                }
                }

                foreach (Buff bb in System.Enum.GetValues(typeof(Buff)))
                    if (buffTrans[(int)bb] == null)
                        linkList[(int)bb].gameObject.SetActive(false);
                    else
                    {
                        linkList[(int)bb].SetPosition(0, transform.position + new Vector3(0, 0.2f, 0));
                        linkList[(int)bb].gameObject.SetActive(true);
                        linkList[(int)bb].SetPosition(1, buffTrans[(int)bb].position + new Vector3(0, 0.2f, 0));
                    }

                buffP = buffList[1] > 0;
                buffS = buffList[0] > 1;
                buffC = buffList[2] > 0;
            }
            else
                HideLine();

            damage = (int)((float)damage + buffList[1]);
            coolTime /= buffList[0];
            coolTime = Mathf.Max(coolTime, 0.001f);
            critical += (int)buffList[2];
        }
    }

    public void HideLine()
    {
        foreach (Buff bb in System.Enum.GetValues(typeof(Buff)))
                linkList[(int)bb].gameObject.SetActive(false);
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
                int nowDamage = damage + PlayerState.Instance.power + levelUpBuff;
                if (towerResource.name != "독 타워")
                    nowDamage += MonsterSpwan.Instance.monsters[i].posionDamage;
                int random = Random.Range(0, 100);
                if (random < critical)
                    nowDamage *= 2;

                for (int j = 0; j < effectStart.Count; j++)
                {
                    if (bullet != Bullet.NoBullet)
                        BulletManager.FireBullet(bullet, effectStart[j].position, effectStart[j].transform.rotation, effectStart[j].transform.localScale, MonsterSpwan.Instance.monsters[i], nowDamage);
                    else
                    {
                        MonsterSpwan.Instance.monsters[i].hp -= nowDamage;
                        MonsterSpwan.Instance.monsters[i].GetDebuff(towerResource.debuffs, towerLevel);
                        if (effect != Effect.NoEffect)
                            EffectManager.EffectRun(effect, effectStart[j].position, effectStart[j].transform.rotation, effectStart[j].transform.localScale);
                    }
                    if (fireEffect != Effect.NoEffect)
                        EffectManager.EffectRun(fireEffect, effectStart[j].position, effectStart[j].transform.rotation, effectStart[j].transform.localScale);
                }
                break;
            }

        //머리 회전
        head.transform.rotation = Quaternion.Lerp(head.transform.rotation, Quaternion.Euler(lookVec), Time.deltaTime * 5);


    }

    private void UpdateMats()
    {
        foreach (TowerLevelMat m in towerLevelMats)
            m.UpdateMat(towerLevel);
    }

    float levelUpTime = 0;
    int levelUpBuff = 0;
    private void LevelUpTowerBuff()
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
    }
}
