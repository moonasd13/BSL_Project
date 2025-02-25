using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    int _invenXMaxSize = 20;
    int _invenYMaxSize = 20;
    int[] _invenSize;

    [SerializeField]
    List<Item> items;
    [SerializeField]
    Grid[] grids;

    void Start()
    {

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
