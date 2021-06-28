using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectPool : SerializedMonoBehaviour
{
    [HideInInspector] protected List<GameObject> objectPool = new List<GameObject>();

    #region[오브풀링에 오브젝트 추가]
    protected void AddObject(GameObject emp, string name)
    {
        emp.transform.name = name;
        objectPool.Add(emp);
    }
    protected void AddObject(GameObject emp)
    {
        objectPool.Add(emp);
    }
    #endregion

    #region[오브젝트풀링에서 오브젝트를 찾아줌]
    //flag == true : 모든객체에서 찾음 // flag == false : 비활성화 객체만 찾음
    protected GameObject FindObject(string s, bool flag = false)
    {
        if (!flag)
        {
            for (int i = 0; i < objectPool.Count; i++)
                if (!objectPool[i].activeSelf && objectPool[i].transform.name.Equals(s))
                    return objectPool[i];
        }
        else
        {
            for (int i = 0; i < objectPool.Count; i++)
                if (objectPool[i].transform.name.Equals(s))
                    return objectPool[i];
        }
        return null;
    }
    #endregion

    #region[오브젝트풀링 데이터를 모두 비활성화]
    protected virtual void PoolOff()
    {
        for (int i = 0; i < objectPool.Count; i++)
            if (objectPool[i])
                objectPool[i].SetActive(false);
    }
    #endregion

    #region[멀리있는 오브젝트 비활성화]
    protected virtual void ObjectAct()
    {
        for (int i = 0; i < objectPool.Count; i++)
            if (objectPool[i])
            {
                Vector3 targetScreenPos = Camera.main.WorldToViewportPoint(objectPool[i].transform.position);

                Vector2 size = new Vector2(1.5f, 1.5f);

                objectPool[i].SetActive(
                    !(
                            targetScreenPos.x > (1 + size.x) / 2f ||
                            targetScreenPos.x < (1 - size.x) / 2f ||
                            targetScreenPos.y > (1 + size.y) / 2f ||
                            targetScreenPos.y < (1 - size.x) / 2f
                    ));
            }
    }
    #endregion
}