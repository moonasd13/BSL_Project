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

    void Start()
    {

    }
}
