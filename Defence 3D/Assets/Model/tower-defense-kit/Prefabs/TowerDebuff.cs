using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Debuff
{
    Freeze,Burn,Posion
}

[Serializable]
public class TowerDebuff
{
    public Debuff debuffType;

    public List<float> value;
}
