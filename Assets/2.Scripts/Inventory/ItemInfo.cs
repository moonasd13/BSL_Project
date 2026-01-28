using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;


public class ItemInfo : MonoBehaviour
{
    ItemName WeaponName;
    ItemType ItemType;
    int price;
    public int itemNumber;
    public int item_InherenceNumber;
    public string itemName;
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
}
