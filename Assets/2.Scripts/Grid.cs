using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Grid : MonoBehaviour
{
    Image Image;                                // 그리드칸의 이미지 변경을 위한 클래스변수
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    ItemInfo item;
    Color SpaecColor = new Color(1f, 1f, 1f, 0.5f);
    //[SerializeField]
    //Color plusColor = new Color(248, 210, 85);  // (+)이미지 색
    public bool isEmpty { get; set; }       // 칸이 비어있는지 확인하는 변수
    void Start()
    {
        Image = gameObject.GetComponent<Image>();
    }
    void SetGrid(ItemInfo item)
    {
        Image.color = SpaecColor;
        this.item = item;
        isEmpty = true;
    }
    void RayCastHitCheck()
    {

    }
    public void Test()
    {
        Debug.Log("충돌체크");
        Image.color = Color.red;
    }
}
