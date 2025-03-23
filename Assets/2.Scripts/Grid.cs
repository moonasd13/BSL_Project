using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Grid : MonoBehaviour
{
    [SerializeField]
    Image Image;                                // 그리드칸의 이미지 변경을 위한 클래스변수
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    GameObject item;
    Color plusColor = new Color(248, 210, 85);  // (+)이미지 색
    public bool isEmpty { get; set; }       // 칸이 비어있는지 확인하는 변수
    public bool isActive { get; set; }                   // 칸이 활성화 되어있는지 확인하는 변수
    public GameObject Item;
    void Start()
    {
        Image = gameObject.GetComponent<Image>();
    }
}
