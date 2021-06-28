using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Buff
{
    Speed,Pow,Critical
}

[Serializable]
public class TowerBuff
{
    public Buff buffType;

    public List<float> value;
}
