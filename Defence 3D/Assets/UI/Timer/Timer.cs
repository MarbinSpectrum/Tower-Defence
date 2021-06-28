using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    public TextMeshProUGUI text;
    public Animator animator;

    public static float time;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            time = 0;
        }
    }
    private void Update()
    {
        time -= Time.deltaTime;
        text.text = ((int)time).ToString();
        if (time < 0 && animator.GetBool("Open"))
            animator.SetBool("Open", false);
        else if (time > 0 && !animator.GetBool("Open"))
            animator.SetBool("Open", true);
    }
}
