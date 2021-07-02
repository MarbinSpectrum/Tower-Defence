using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[Serializable]
public class MonsterGroup
{
    [TableList(DrawScrollView = true)]
    public List<Monster> monsters = new List<Monster>();
}
