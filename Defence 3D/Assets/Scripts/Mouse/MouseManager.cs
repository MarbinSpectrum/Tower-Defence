using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : Singleton<MouseManager>
{
    public static Vector3 mousePos;
    public static Vector2Int nowTile;
    public static bool isDragging = false;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        int layerMask = 1 << LayerMask.NameToLayer("Ground");

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100, layerMask))
        { 
            mousePos = hit.point;
            nowTile = new Vector2Int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.z));
        }
    }
}
