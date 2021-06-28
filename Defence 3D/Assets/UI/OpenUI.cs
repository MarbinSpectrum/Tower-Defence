using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUI<T> : MonoBehaviour where T : OpenUI<T>
{
    public Animator animator;
    public static T Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = (T)this;
    }
    public static void SetUIState(bool b) => Instance.animator.SetBool("Open", b);
}
