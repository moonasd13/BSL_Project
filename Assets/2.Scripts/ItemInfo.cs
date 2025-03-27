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

    void Start()
    {
        // юс╫ц
        //tempItem = new int[4, 3] { { 1, 1, 1 }, { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
        cellData = new ItemCellData();
        //cellData.SetItem(3, 4, tempItem);
    }
}
