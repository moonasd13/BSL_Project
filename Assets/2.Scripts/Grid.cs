using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
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
    public void OnBeginDrag(PointerEventData eventData) // 눌렀을때
    {

    }
    public void OnDrag(PointerEventData eventData)      // 드래그 중일때
    {

    }
    public void OnEndDrag(PointerEventData eventData)   // 드래그가 끝났을때
    {

    }
    public void OnDrop(PointerEventData eventData)
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
