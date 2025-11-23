using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;


public class ItemInfo : MonoBehaviour
{
    ItemName WeaponName;
    //ItemCellData cellData;
    RectTransform rectTransform;
    //[SerializeField]
    //int width;
    //[SerializeField]
    //int height;
    [SerializeField]
    Grid[] curGetGrids;        // 아이템이 현재 점유하고있는 그리드

    void Start()
    {
        Temp();
    }

    void Temp()
    {
        // 임시
        if (WeaponName == ItemName.Axe)
        {
            name = "도끼";
        }
        else if (WeaponName == ItemName.HockeyStick)
        {
            name = "하키채";
        }
        else if (WeaponName == ItemName.SniperRifle)
        {
            name = "저격총";
        }
    }
    public void GetGrids(Grid[] grid)
    {
        curGetGrids = new Grid[grid.Length];
        for (int i = 0; i < grid.Length; i++)
        {
            curGetGrids[i] = grid[i];
        }
    }
    public Grid[] curGrids()
    {
        return curGetGrids;
    }
    public bool AreGridsEqual(EmptyCheck emptyCheck)
    {
        if (curGetGrids.Length != emptyCheck.gridRayChecks.Length)
            return false;

        for (int i = 0; i < curGetGrids.Length; i++)
        {
            if (curGetGrids[i] != emptyCheck.gridRayChecks[i].hitGrid)
            {
                return false;
            }
        }
        return true;
    }
    public void GetComponentInventorySlotDropHandler()
    {
        gameObject.GetComponent<InventorySlotDropHandler>();
    }
}
