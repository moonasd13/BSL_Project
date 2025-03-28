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

    void Start()
    {
        tempItem = new int[width, height];
        cellData = new ItemCellData();
        //cellData.SetItem(width, height, tempItem);
    }
}
