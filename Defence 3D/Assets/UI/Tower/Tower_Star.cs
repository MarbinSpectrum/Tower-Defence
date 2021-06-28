using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

public class Tower_Star : MonoBehaviour
{
    private List<TowerStarView> star = new List<TowerStarView>();
    private List<RectTransform> starRectTransform = new List<RectTransform>();

    public TowerStarView barUI;

    public CanvasGroup canvasGroup;

    public Vector3 offset;



    private void Update()
    {
        if (CameraManager.nowZoom)
            canvasGroup.alpha = 0;
        else
            canvasGroup.alpha = 1;

        while (star.Count < TowerManager.Instance.towerObj.Count)
        {
            TowerStarView temp = Instantiate(barUI).GetComponent<TowerStarView>();
            temp.transform.SetParent(transform);
            star.Add(temp);
            RectTransform rectTransform = temp.GetComponent<RectTransform>();
            starRectTransform.Add(rectTransform);
        }

        for (int i = 0; i < star.Count; i++)
        {
            if (!Exception.IndexOutRange(i, TowerManager.Instance.towerObj))
                star[i].gameObject.SetActive(false);
            else if(TowerManager.Instance.towerObj[i] == null)
                star[i].gameObject.SetActive(false);
            else
            {
                star[i].gameObject.SetActive(true);
                star[i].transform.position = TowerManager.Instance.towerObj[i].transform.position + offset;
                star[i].level = TowerManager.Instance.towerObj[i].towerLevel;
                starRectTransform[i].localRotation = Quaternion.Euler(0, 0, 0);
                starRectTransform[i].localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
