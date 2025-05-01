using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemInfo : MonoBehaviour
{
    ItemCellData cellData;
    RectTransform rectTransform;
    [SerializeField]
    int width;
    [SerializeField]
    int height;
    [SerializeField]
    string itemName;
    [SerializeField]
    Grid[] curGetGrids;        // 아이템이 현재 점유하고있는 그리드

    void Start()
    {
        Temp();
    }

    void Temp()
    {
        // 임시
        width = 3;
        height = 4;
        name = "도끼";
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

}
