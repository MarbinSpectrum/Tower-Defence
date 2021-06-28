using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject grid;
    public GameObject green;
    public GameObject red;
    public GameObject white;

    public bool towerExist
    {
        get { return tower != null; }
    }

    public GameObject tower;

    private void Update()
    {
        grid.SetActive(MouseManager.isDragging);
        bool thisTile = (transform.position.x == MouseManager.nowTile.x && transform.position.z == MouseManager.nowTile.y);
        
        if(Drag.nowDrag != null)
        {
            green.SetActive(!towerExist && MouseManager.isDragging && thisTile);
            red.SetActive(towerExist && MouseManager.isDragging && thisTile);
            white.SetActive(false);
        }
        else if (TowerDrag.nowDrag != null)
        {
            green.SetActive(false);
            red.SetActive(false);
            white.SetActive(MouseManager.isDragging && thisTile);
        }
        else
        {
            green.SetActive(false);
            red.SetActive(false);
            white.SetActive(false);
        }
    }


}
