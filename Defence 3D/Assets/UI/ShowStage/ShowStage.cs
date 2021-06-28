using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowStage : MonoBehaviour
{
    public TextMeshProUGUI text;

    public CanvasGroup canvasGroup;

    public float speed = 0.05f;

    private void Update()
    {

        text.text = "스테이지 " + (TurnManager.Instance.turnNum).ToString();
        if (!CameraManager.nowZoom)
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, speed);
        else
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, speed);
    }

}
