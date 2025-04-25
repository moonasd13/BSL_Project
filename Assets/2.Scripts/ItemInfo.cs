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
    int[,] tempItem;
    public string name;
    [SerializeField]
    Grid[] getGrids;

    void Start()
    {

    }
    public void SetGrids(Grid[] grid)
    {
        getGrids = new Grid[grid.Length];
        for (int i = 0; i < grid.Length; i++)
        {
            getGrids[i] = grid[i];
        }
    }
}
