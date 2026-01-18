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
    public int itemNumber;
    public string itemName;
    //ItemCellData cellData;
    RectTransform rectTransform;
    [SerializeField]
    GameObject[] ItemPrefab;
    [SerializeField]
    Grid[] curGetGrids;        // 아이템이 현재 점유하고있는 그리드

    //ItemState curItemState;

    void Start()
    {
        //itemRan = UnityEngine.Random.Range(0, 3);
        //Temp(itemRan);
    }

    //void Temp(int index)
    //{
    //    // 임시
    //    if (itemRan == 0)
    //    {
    //        WeaponName = ItemName.Axe;
    //        name = "도끼";
    //    }
    //    else if (itemRan == 1)
    //    {
    //        WeaponName = ItemName.HockeyStick;
    //        name = "하키채";
    //    }
    //    else if (itemRan == 2)
    //    {
    //        WeaponName = ItemName.SniperRifle;
    //        name = "저격총";
    //    }
    //}
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
    //public void GetComponentInventorySlotDropHandler()
    //{
    //    gameObject.GetComponent<InventorySlotDropHandler>();
    //}
}
