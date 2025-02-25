using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    int _invenMaxSize = 64;
    int[] _inven;
    Grid[] grids;

    void Start()
    {

    }
    void InitInvenSize()
    {
        _inven = new int[_invenMaxSize];
        for (int i = 0; i < _invenMaxSize; i++)
        {
            grids[i] = transform.GetChild(0).GetChild(i).GetComponent<Grid>();
        }
        for (int i = 0; i < _invenMaxSize; i++)
        {
            //if ()
            grids[i].SetTile();
        }
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
