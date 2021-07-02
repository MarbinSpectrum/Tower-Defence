using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum SE
{
    boom,blood,laser,arrow,selectMenu,clickBtn,cantRun,Gun,Gold,Cannon,ObjMove,Error,Gas, Flamethrower,LevelUp,GameOver
}

public enum BGM
{
    Sorrowful, Soldiers ,Boss
}

public class SoundManager : Singleton<SoundManager>
{
    [DictionaryDrawerSettings(KeyLabel = "SE", ValueLabel = "Sound")]
    public Dictionary<SE, AudioClip> SE = new Dictionary<SE, AudioClip>();

    [DictionaryDrawerSettings(KeyLabel = "BGM", ValueLabel = "Sound")]
    public Dictionary<BGM, AudioClip> BGM = new Dictionary<BGM, AudioClip>();

    [Header("----------------------------")]

    public AudioSource seA;
    public AudioSource bgmA;

    private Dictionary<SE, int> SE_count = new Dictionary<SE, int>();
    public override void Awake()
    {
        base.Awake();
        foreach (SE s in Enum.GetValues(typeof(SE)))
            SE_count[s] = 0;
    }

    public static void PlayBGM(BGM bg)
    {
        if(Instance.bgmA.clip != null && Instance.bgmA.clip == Instance.BGM[bg])
            return;
        Instance.bgmA.clip = Instance.BGM[bg];
        Instance.bgmA.Play();
    }


    public static void StopBGM()
    {
        Instance.bgmA.Stop();
        Instance.bgmA.clip = null;
    }

    public static void PlaySE(SE se)
    {
        if(Instance.SE_count[se] < 20)
        {
            Instance.CheckSE(se);
            Instance.seA.PlayOneShot(Instance.SE[se]);
        }
    }

    private void CheckSE(SE se) => StartCoroutine(Cor_SE_Check(se));
    IEnumerator Cor_SE_Check(SE se)
    {
        SE_count[se]++;
        float time = Instance.SE[se].length;
        yield return new WaitForSeconds(time);
        SE_count[se]--;
    }
}
