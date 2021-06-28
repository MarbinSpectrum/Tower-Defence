using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Custom;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler
{
    public static Drag nowDrag;

    public DragAndDropContainer dragAndDropContainer;
    public TowerSlot isTowerSlot;
    public GameObject nowTower;


    public static void CancleDrag()
    {
        if (nowDrag == null)
            return;

        nowDrag.isTowerSlot.hide = false;
        MouseManager.isDragging = false;
        CameraManager.ChangeZoom(CameraManager.ZOOM_IN);

        nowDrag.dragAndDropContainer.gameObject.SetActive(false);

        nowDrag = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        nowDrag = this;
        dragAndDropContainer.gameObject.SetActive(true);
        nowTower = isTowerSlot.towerResource.obj;

        //드래그객체 초기화
        var child = dragAndDropContainer.transform.GetComponentsInChildren<Transform>();
        foreach (var iter in child)
            if (iter != dragAndDropContainer.transform)
                Destroy(iter.gameObject);

        //드래그객체 설정
        GameObject temp = Instantiate(nowTower);
        temp.transform.SetParent(dragAndDropContainer.transform);
        temp.transform.localPosition = Vector3.zero;

        isTowerSlot.hide = true;
        MouseManager.isDragging = true;
        CameraManager.ChangeZoom(CameraManager.ZOOM_OUT);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (nowDrag == null)
            return;

        nowDrag.isTowerSlot.hide = false;
        MouseManager.isDragging = false;
        CameraManager.ChangeZoom(CameraManager.ZOOM_IN);

        nowDrag.dragAndDropContainer.gameObject.SetActive(false);

        nowDrag = null;

        TileManager.TowerSet(nowTower, isTowerSlot);
    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}
