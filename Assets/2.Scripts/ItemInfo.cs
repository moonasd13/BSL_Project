using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;


public class ItemInfo : MonoBehaviour
{
    ItemName WeaponName;
    ItemType ItemType;
    int itemRan;
    int price;
    public int itemNumber;
    public int item_InherenceNumber;
    public string itemName;
    //ItemCellData cellData;
    RectTransform rectTransform;
    [SerializeField]
    GameObject[] ItemPrefab;
    [SerializeField]
    Grid[] curGetGrids;        // 아이템이 현재 점유하고있는 그리드

    bool ItemForInven;

    void Start()
    {

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
