using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

public class Tower_UI : MonoBehaviour
{
    public TowerView towerUI;

    public TowerObject towerObject;

    //private void Awake() => GetComponent<Canvas>().worldCamera = CameraManager.Instance.camera;

    private void Update()
    {
        towerUI.ui_Group[0].SetActive(!CameraManager.nowZoom);

        towerUI.level = towerObject.towerLevel;
        towerUI.State[Buff.Speed] = towerObject.buffS;
        towerUI.State[Buff.Pow] = towerObject.buffP;
        towerUI.State[Buff.Critical] = towerObject.buffC;

    }
}
