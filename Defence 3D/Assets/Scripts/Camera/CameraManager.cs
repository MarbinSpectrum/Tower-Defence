using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public Rigidbody rigidbody;

    public Camera camera;
    private static float size = 4;

    public const float ZOOM_IN = 4;
    public const float ZOOM_OUT = 6f;

    public static bool nowZoom
    {
        get { return size == ZOOM_IN; }
    }

    private void Awake()
    {
        Instance = this;
        size = ZOOM_IN;  
    }

    public float Speed = 2f;
    private Vector3 nowPos, prePos;
    private Vector3 movePos;

    private void Update()
    {
        camera.orthographicSize = (float)Mathf.Lerp(camera.orthographicSize, size, 0.05f);

        if (nowZoom && Drag.nowDrag == null && TowerDrag.nowDrag == null)
        {
            if (Input.GetMouseButtonDown(0))
                prePos = MouseManager.mousePos;
            else if (Input.GetMouseButton(0))
            {
                nowPos = MouseManager.mousePos;
                Vector3 dir = prePos - nowPos;

                rigidbody.velocity = (dir * Time.deltaTime) * Speed;
            }
            else
            {
                rigidbody.velocity = Vector3.zero;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, offset, 0.05f);
        }
    }

    public Vector3 offset = new Vector3(0.6f,0,0.3f);

    public static void ChangeZoom(float zoom)
    {
        size = zoom;
    }


}
