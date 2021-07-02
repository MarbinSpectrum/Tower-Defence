using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public static GameOverScreen Instacne;

    private Texture2D ScreenTexture;
    public Image grayScreen;

    private Animator animator;
    private bool retry = false;

    private void Awake()
    {
        if (Instacne == null)
            Instacne = this;
        animator = grayScreen.GetComponent<Animator>();
    }

    public void GameOver() => StartCoroutine(CaptureScreen());

    IEnumerator CaptureScreen()
    {
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();

        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        texture.Apply();
        ScreenTexture = texture;
        grayScreen.sprite = Sprite.Create(ScreenTexture, new Rect(0, 0, ScreenTexture.width, ScreenTexture.height), Vector2.zero);
        grayScreen.gameObject.SetActive(true);
    }

    public void Retry()
    {
        if (retry)
            return;
        retry = true;
        animator.SetTrigger("Retry");
        StartCoroutine(RetryGame());
    }

    IEnumerator RetryGame()
    {
        yield return new WaitWhile(() => { return !animator.GetCurrentAnimatorStateInfo(0).IsName("Retry") || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99; });

        SceneManager.LoadScene("InGame");
    }
}
