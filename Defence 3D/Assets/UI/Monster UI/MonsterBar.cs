using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MonsterBar : MonoBehaviour
{
    public Image bar;

    public int maxHP;
    public int nowHP;

    public Dictionary<Debuff, bool> State = new Dictionary<Debuff, bool>();

    public List<GameObject> debuff_UI = new List<GameObject>();


    private void Start()
    {
        foreach (Debuff debuff in Enum.GetValues(typeof(Debuff)))
            State[debuff] = false;
    }

    private void Update()
    {
        bar.fillAmount = ((float)nowHP / (float)maxHP);

        for (int i = 0; i < debuff_UI.Count; i++)
            debuff_UI[i].SetActive(State[(Debuff)i]);
    }


}
