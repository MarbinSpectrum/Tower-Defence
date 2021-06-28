using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    public Transform rangeObject;
    public Vector3 baseSize;
    public TowerObject towerObject;
    private void Update()
    {
        if (towerObject.towerResource == null)
            return;

        if (!CameraManager.nowZoom || towerObject.towerResource.buffTower)
            rangeObject.gameObject.SetActive(true);
        else
            rangeObject.gameObject.SetActive(false);

        rangeObject.localScale = baseSize * towerObject.range;
    }
}
