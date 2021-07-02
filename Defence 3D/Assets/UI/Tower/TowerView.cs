using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerView : MonoBehaviour
{
    public Dictionary<Buff, bool> State = new Dictionary<Buff, bool>();

    [NonSerialized]
    public int level;

    public List<GameObject> ui_Group = new List<GameObject>();

    public List<Sprite> buff_Sprite = new List<Sprite>();

    public List<Transform> ui_Pos = new List<Transform>();

    private List<GameObject> stars = new List<GameObject>();
    private List<SpriteRenderer> buff_UI = new List<SpriteRenderer>();

    private List<Vector3> uiPos = new List<Vector3>();

    private void Awake()
    {
        foreach (Buff buff in Enum.GetValues(typeof(Buff)))
            State[buff] = false;
        for (int i = 0; i < ui_Group[0].transform.childCount; i++)
            stars.Add(ui_Group[0].transform.Find((i+1)+"Star").gameObject);
        for (int i = 0; i < ui_Group[1].transform.childCount; i++)
        {
            buff_UI.Add(ui_Group[1].transform.GetChild(i).GetComponent<SpriteRenderer>());
            uiPos.Add(ui_Group[1].transform.Find((i + 1).ToString()).transform.localPosition);
        }
        uiPos.Add(Vector3.zero);

    }

    private void Update()
    {
        List<int> act_UI_group = new List<int>();
        for (int i = 0; i < ui_Group.Count; i++)
            if (ui_Group[i].activeSelf)
                act_UI_group.Add(i);
        for (int i = 0; i < act_UI_group.Count; i++)
            ui_Group[act_UI_group[i]].transform.localPosition = ui_Pos[i].transform.localPosition;


        for (int i = 0; i < stars.Count; i++)
            stars[i].SetActive(i == level);


        List<int> nowBuff = new List<int>();
        foreach (Buff buff in Enum.GetValues(typeof(Buff)))
            if (State[buff])
                nowBuff.Add((int)buff);
        for (int i = 0; i < buff_UI.Count; i++)
            buff_UI[i].gameObject.SetActive(false);
        for (int i = 0; i < nowBuff.Count; i++)
        {
            buff_UI[i].sprite = buff_Sprite[nowBuff[i]];
            buff_UI[i].gameObject.SetActive(true);
        }
        float per = (float)1 / (float)(buff_UI.Count - 1);
        for (int i = 0; i < nowBuff.Count; i++)
            buff_UI[i].transform.localPosition = Vector3.Lerp(uiPos[i], uiPos[i + 1], 1 - per * (nowBuff.Count - 1));
    }
}
