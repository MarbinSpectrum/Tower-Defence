using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_HP : MonoBehaviour
{
    private List<MonsterBar> bar = new List<MonsterBar>();
    private List<RectTransform> barRectTransform = new List<RectTransform>();

    public MonsterBar barUI;

    public Vector3 offset;

    private void Update()
    {
        while(bar.Count < MonsterSpwan.Instance.monsters.Count)
        {
            MonsterBar temp = Instantiate(barUI).GetComponent<MonsterBar>();
            temp.transform.SetParent(transform);
            bar.Add(temp);
            RectTransform rectTransform = temp.GetComponent<RectTransform>();
            barRectTransform.Add(rectTransform);

        }

        for (int i = 0; i < Mathf.Min(MonsterSpwan.Instance.monsters.Count, bar.Count); i++)
        {
            if (MonsterSpwan.Instance.monsters[i] == null)
                bar[i].gameObject.SetActive(false);
            else
            {
                bar[i].gameObject.SetActive(true);
                bar[i].transform.position = MonsterSpwan.Instance.monsters[i].transform.position + offset;
                bar[i].maxHP = MonsterSpwan.Instance.monsters[i].maxhp;
                bar[i].nowHP = MonsterSpwan.Instance.monsters[i].hp;
                bar[i].State[Debuff.Freeze] = MonsterSpwan.Instance.monsters[i].nowFreeze;
                bar[i].State[Debuff.Burn] = MonsterSpwan.Instance.monsters[i].nowBurn;
                bar[i].State[Debuff.Posion] = MonsterSpwan.Instance.monsters[i].nowPosion;
                barRectTransform[i].localRotation = Quaternion.Euler(0,0,0);
                barRectTransform[i].localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
