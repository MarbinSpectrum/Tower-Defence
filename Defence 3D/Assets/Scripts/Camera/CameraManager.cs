using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public Camera camera;
    public static float size = 4;

    public const float ZOOM_IN = 3;
    public const float ZOOM_OUT = 4.5f;

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
    public Vector2 maxV, minV;
    private Vector3 basePos;

    private void Update()
    {
        if (PlayerState.Instance.gameOver)
            return;

        camera.orthographicSize = (float)Mathf.Lerp(camera.orthographicSize, size, 0.05f);

        if (nowZoom && Drag.nowDrag == null && TowerDrag.nowDrag == null)
        {
            if (Input.GetMouseButtonDown(0))
                prePos = MouseManager.mousePos;
            else if (Input.GetMouseButton(0))
            {
                basePos = Vector3.zero;
                nowPos = MouseManager.mousePos;
                Vector3 dir = prePos - nowPos;
                Vector3 moveValue = (dir * (0.002f)) * Speed;

                Vector3 frontLocal = camera.transform.localPosition;
                camera.transform.Translate(moveValue, Space.World);
                Vector3 nextLocal = camera.transform.localPosition;

                if (camera.transform.localPosition.z > maxV.y || camera.transform.localPosition.z < minV.y)
                    camera.transform.localPosition -= new Vector3(0, 0, nextLocal.z - frontLocal.z);

                if (camera.transform.localPosition.x > maxV.x || camera.transform.localPosition.x < minV.x)
                    camera.transform.localPosition -= new Vector3(nextLocal.x - frontLocal.x, nextLocal.y - frontLocal.y, 0);

            }
            else
            {
                if (basePos == Vector3.zero)
                    basePos = camera.transform.position;
                camera.transform.position = Vector3.Lerp(camera.transform.position, basePos, 0.05f);
            }
        }
        else
        {
            if (basePos == Vector3.zero)
                basePos = camera.transform.position;
            camera.transform.position = Vector3.Lerp(camera.transform.position, offset, 0.05f);
        }
    }

    public Vector3 offset = new Vector3(0.6f,0,0.3f);

    public static void ChangeZoom(float zoom)
    {
        size = zoom;
    }

    public static void VibrationCamera(float pow) => Instance.pVibrationCamera(pow);

    private void pVibrationCamera(float pow) => StartCoroutine(Cor_VibrationCamera(pow));

    IEnumerator Cor_VibrationCamera(float pow = 1,int count = 20)
    {
        Vector3 baseV = transform.position;
        for (int i = 0; i < count; i++)
        {
            pow = Mathf.Lerp(pow, 0, (float)i / (float)count);
            Vector3 temp = baseV + new Vector3(Random.Range(-pow, pow), 0, Random.Range(-pow, pow));
            transform.position = temp;
            yield return new WaitForSeconds(0.02f);
        }
        transform.position = baseV;
    }

}
