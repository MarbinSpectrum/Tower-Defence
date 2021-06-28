using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletObj : MonoBehaviour
{
    [NonSerialized]
    public Vector3 pos;
    public Vector3 offset;

    public MonsterObect monsterObect;

    public Effect effect;

    public int damage;

    [Range(0,1)]
    public float speed = 0.1f;
    public float nowSpeed = 0;

    public GameObject body;

    public void Update()
    {
        if (monsterObect != null)
            pos = monsterObect.transform.position;
        nowSpeed += Time.deltaTime/10f;
        nowSpeed = Mathf.Min(1, nowSpeed);
        Vector3 d = pos + offset;

        transform.position = Vector3.Lerp(transform.position, d, nowSpeed);
        transform.LookAt(d);
        body.gameObject.SetActive(true);
        if (Vector3.Distance(transform.position,d) < 0.5f)
        {
            EffectManager.EffectRun(effect, d);
            monsterObect.hp -= damage;
            body.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        nowSpeed = speed;
    }
}
