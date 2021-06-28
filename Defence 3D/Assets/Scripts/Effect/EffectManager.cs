using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum Effect
{
    NoEffect,Explosion0,Fire,Ice,Posion,Blood,Bullet,FPS,Laser
}

public class EffectManager : ObjectPool
{
    [DictionaryDrawerSettings(KeyLabel = "이펙트 이름", ValueLabel = "오브젝트")]
    public Dictionary<Effect, GameObject> effectObject = new Dictionary<Effect, GameObject>();

    public static EffectManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }
    }

    public void pEffectRun(Effect effect, Vector3 start,Quaternion quaternion,Vector3 size)
    {
        string name = effect.ToString();

        GameObject emp = FindObject(name);

        if (emp == null)
        {
            emp = Instantiate(effectObject[effect]);
            emp.transform.name = name;
            AddObject(emp);
        }

        emp.gameObject.SetActive(true);
        emp.transform.localRotation = quaternion;
        emp.transform.localScale = size;
        emp.transform.parent = transform;
        emp.transform.position = start;
    }

    public static void EffectRun(Effect effect, Vector3 start, Quaternion quaternion, Vector3 size) => Instance.pEffectRun(effect, start, quaternion, size);
    public static void EffectRun(Effect effect, Vector3 start) => EffectRun(effect, start, Quaternion.identity, new Vector3(1, 1, 1));
}
