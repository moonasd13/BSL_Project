using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemInfo : MonoBehaviour
{
    ItemCellData cellData;
    RectTransform rectTransform;

    int width;

    int height;
    int[,] tempItem;
    string name;
    [SerializeField]
    Grid[] getGrids;        // 아이템이 현재 점유하고있는 그리드

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
    public void SetGrids(Grid[] grid)
    {
        getGrids = new Grid[grid.Length];
        for (int i = 0; i < grid.Length; i++)
        {
            getGrids[i] = grid[i];
        }
    }
    public bool AreGridsEqual(EmptyCheck emptyCheck)
    {
        if (getGrids.Length != emptyCheck.gridRayChecks.Length)
            return false;

        for (int i = 0; i < getGrids.Length; i++)
        {
            if (getGrids[i] != emptyCheck.gridRayChecks[i].hitGrid)
            {
                return false;
            }
        }
        return true;
    }

}
