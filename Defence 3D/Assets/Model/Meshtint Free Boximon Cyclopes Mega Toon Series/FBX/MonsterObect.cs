using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterObect : MonoBehaviour
{
    [NonSerialized]
    public int movePos = CreateMap.DESTINATION;

    [NonSerialized]
    public int hp = 5;

    [NonSerialized]
    public int maxhp = 5;

    [NonSerialized]
    public float speed;

    private Vector3 moveVec;

    public void Update()
    {
        burnTime += Time.deltaTime;
        if (burnTime > 1 && burnDamage > 0)
        {
            hp -= burnDamage;
            burnTime = 0;
        }

        //���� ü���� ������ ����
        if (hp <= 0)
        {
            MonsterSpwan.existMonster--;
            Destroy(gameObject);
        }


        //���� ���� ����
        if (movePos == 0)
            moveVec = CreateMap.EndPos;
        else
            moveVec = CreateMap.GetPosVector(movePos);

        //���� �̵�
        Vector3 dic = (moveVec - transform.position).normalized;
        float nowSpeed = Mathf.Max(1, speed - freezeSlow);
        Vector3 nextPos = transform.position + dic * nowSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, moveVec) < Vector3.Distance(transform.position, nextPos))
            transform.position = moveVec;
        else
            transform.position = nextPos;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVec - transform.position), Time.deltaTime * 5);



        if (Vector3.Distance(moveVec, transform.position) < 0.1)
        {
            movePos = CreateMap.NextMovePos(movePos);
            if (movePos == -1)
            {
                PlayerState.Instance.hp--;
                MonsterSpwan.existMonster--;
                Destroy(gameObject);
            }
        }

    }

    private int freezeSlow = 0;

    private int burnDamage = 0;
    private float burnTime = 0;

    [NonSerialized]
    public int posionDamage = 0;

    public void GetDebuff(List<TowerDebuff> debuffs, int towerLevel)
    {
        foreach (TowerDebuff debuff in debuffs)
        {
            if (debuff.debuffType == Debuff.Burn)
                burnDamage = Mathf.Max(burnDamage, (int)debuff.value[towerLevel]);
            else if (debuff.debuffType == Debuff.Freeze)
                freezeSlow = Mathf.Max(freezeSlow, (int)debuff.value[towerLevel]);
            else if (debuff.debuffType == Debuff.Posion)
                posionDamage = Mathf.Max(posionDamage, (int)debuff.value[towerLevel]);
        }
    }
}
