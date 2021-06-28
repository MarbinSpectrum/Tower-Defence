using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_View : MonoBehaviour
{
    public GameObject viewUI;
    public void SetView(bool b)
    {
        if (b && MouseManager.isDragging)
            return;
        viewUI.SetActive(b);
    }
}
