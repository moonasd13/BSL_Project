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
    Grid[] curGetGrids;        // �������� ���� �����ϰ��ִ� �׸���

    void Start()
    {
        Temp();
    }

    void Temp()
    {
        // �ӽ�
        width = 3;
        height = 4;
        name = "����";
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
