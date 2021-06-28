using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerData", order = 1)]
public class TowerResource : ScriptableObject
{
    public int level;
    public GameObject obj;
    public string name;
    public Sprite img;

    [Header("---------------------------")]

    public List<float> coolTime;
    public List<int> damage;
    public List<float> range;
    public List<int> critical;

    [Header("---------------------------")]

    [TableList(DrawScrollView = true)]
    public List<TowerDebuff> debuffs;

    [Header("---------------------------")]

    public bool buffTower;

    [TableList(DrawScrollView = true)]
    public List<TowerBuff> buffs;   

    [Header("---------------------------")]

    public bool levelUpTower;
    public List<int> levelUpTowerBuff;

    [TextArea(0,8)]
    public string explain;
}
