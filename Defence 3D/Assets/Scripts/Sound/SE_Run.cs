using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum RunType
{
    Enable, Awake, Event, PointDown, PointClick, BeginDrag
}

public class SE_Run : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IBeginDragHandler
{
    public RunType runType;

    public SE sE;

    private void Awake()
    {
        if (runType != RunType.Awake)
            return;
        RunEvent();
    }

    private void OnEnable()
    {
        if (runType != RunType.Enable)
            return;
        RunEvent();
    }

    public void RunEvent()
    {
        if (SoundManager.Instance == null)
            return;
        SoundManager.PlaySE(sE);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (runType != RunType.PointDown)
            return;
        RunEvent();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (runType != RunType.PointClick)
            return;
        RunEvent();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (runType != RunType.BeginDrag)
            return;
        RunEvent();
    }
}
