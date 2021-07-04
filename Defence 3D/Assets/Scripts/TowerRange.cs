using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    public Transform rangeObject;
    public Vector3 baseSize;
    public TowerObject towerObject;
    private float nowRange;
    private void Update()
    {
        rangeObject.gameObject.SetActive(TowerDrag.nowDrag != null && TowerDrag.nowDrag.gameObject == gameObject);

        if (towerObject.towerResource == null)
            return;

        if(nowRange != towerObject.range)
        {
            nowRange = towerObject.range;
            rangeObject.localScale = baseSize * nowRange;
        }
    }
}
