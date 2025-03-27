using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ItemCellData : MonoBehaviour
{
    string _IdName { get; set; }
    int width { get; set; }
    int height { get; set; }
    int[,] size { get; set; }

    public void SetItem(int width, int height, int[,] shape)
    {
        this.width = width;
        this.height = height;
        size = new int[this.width, this.height];
        for (int y = 0; y < this.height; y++)
        {
            for (int x = 0; x < this.width; x++)
            {
                size[y, x] = shape[y, x];
            }
        }
    }

    public void SwitchRotate()
    {
        int[,] rotated = new int[this.width, this.height]; // 크기를 반대로

        for (int y = 0; y < this.height; y++)
        {
            for (int x = 0; x < this.width; x++)
            {
                rotated[x, this.height - 1 - y] = size[y, x]; // 90도 회전 로직
            }
        }

        // 새로운 배열 적용
        size = rotated;

        // 가로, 세로 변경
        int temp = this.width;
        this.width = this.height;
        this.height = temp;
    }
}
