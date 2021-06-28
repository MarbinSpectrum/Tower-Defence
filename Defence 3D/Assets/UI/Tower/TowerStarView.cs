using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStarView : MonoBehaviour
{
    public int level;
    public List<GameObject> stars = new List<GameObject>();
    void Update()
    {
        for (int i = 0; i < stars.Count; i++)
            stars[i].SetActive(i <= level);
    }
}
