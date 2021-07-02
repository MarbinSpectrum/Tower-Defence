using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStop : MonoBehaviour
{
    public GameObject dontTouch;
    public Image btnImg;

    public Sprite stopImg;
    public Sprite playImg;

    private void Update()
    {
        if (PlayerState.Instance.gameOver)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
            StopGame();
    }

    public void StopGame()
    {
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        dontTouch.gameObject.SetActive(Time.timeScale == 0);
        btnImg.sprite = (Time.timeScale == 0) ? playImg : stopImg;
    }
}
