using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Grid : MonoBehaviour
{
    Image Image;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    ItemInfo item;
    Color SpaecColor = new Color(1f, 1f, 1f, 0.5f);
    public bool isEmpty { get; set; }       // 칸이 비어있는지 확인하는 변수
    //public bool isEmpty = true;
    void Start()
    {
        isEmpty = true;
        Image = gameObject.GetComponent<Image>();
    }
    public void SetGrid(ItemInfo item)
    {
        Image.color = SpaecColor;
        this.item = item;
        isEmpty = true;
    }
}
