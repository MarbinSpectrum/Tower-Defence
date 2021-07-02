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
        if (towerObject.towerResource == null)
            return;

        rangeObject.gameObject.SetActive(!CameraManager.nowZoom || towerObject.towerResource.buffTower);

        if(nowRange != towerObject.range)
        {
            nowRange = towerObject.range;
            rangeObject.localScale = baseSize * nowRange;
        }
    }
}
