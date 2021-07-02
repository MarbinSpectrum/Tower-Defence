using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class Singleton<T> : SerializedMonoBehaviour where T : Singleton<T>
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("@" + typeof(T).ToString(), typeof(T)).AddComponent<T>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(instance);
        }
    }
}

