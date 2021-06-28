using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum Bullet
{
    NoBullet,CannonBall,Arrow,Bullet,Laser
}

public class BulletManager : Singleton<BulletManager>
{
    [DictionaryDrawerSettings(KeyLabel = "총알이름",ValueLabel = "오브젝트")]
    public Dictionary<Bullet, BulletObj> effectObject = new Dictionary<Bullet, BulletObj>();

    private List<BulletObj> objectPool = new List<BulletObj>();

    private void pFireBullet(Bullet bullet,Vector3 start, Quaternion quaternion, Vector3 size, MonsterObect monster,int damage)
    {
        string name = bullet.ToString();

        BulletObj emp = FindObject(name);

        if (emp == null)
        {
            emp = Instantiate(effectObject[bullet]);
            emp.transform.name = name;
            AddObject(emp);
        }

        emp.gameObject.SetActive(true);
        emp.transform.parent = transform;
        emp.transform.position = start;
        emp.transform.localRotation = quaternion;
        emp.transform.localScale = size;
        emp.monsterObect = monster;
        emp.damage = damage;
    }

    public static void FireBullet(Bullet bullet, Vector3 start, MonsterObect monster, int damage) => Instance.pFireBullet(bullet, start,Quaternion.identity,new Vector3(1,1,1), monster, damage);
    public static void FireBullet(Bullet bullet, Vector3 start,Quaternion quaternion,Vector3 size, MonsterObect monster, int damage) => Instance.pFireBullet(bullet, start,quaternion,size, monster, damage);

    private BulletObj FindObject(string s, bool flag = false)
    {
        for (int i = 0; i < objectPool.Count; i++)
            if (flag || !objectPool[i].gameObject.activeSelf)
                if (objectPool[i].transform.name.Equals(s))
                    return objectPool[i];
        return null;
    }

    private void AddObject(BulletObj emp, string name)
    {
        emp.transform.name = name;
        objectPool.Add(emp);
    }
    private void AddObject(BulletObj emp)
    {
        objectPool.Add(emp);
    }
}
