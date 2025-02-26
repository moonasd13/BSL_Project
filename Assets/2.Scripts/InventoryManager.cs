using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    int _invenMaxSize = 64;
    int[] _inven;
    [SerializeField]
    GameObject[] gridsObj;
    [SerializeField]
    Grid[] grids;

    void Start()
    {
        InitInvenSize();
        CheckTile();
    }
    void InitInvenSize()
    {
        _inven = new int[_invenMaxSize];
        for (int i = 0; i < _invenMaxSize; i++)
        {
            gridsObj[i] = transform.GetChild(0).GetChild(i).GetComponent<GameObject>();
            grids[i] = gridsObj[i].GetComponent<Grid>();
            //grids[i] = transform.GetChild(0).GetChild(i).GetComponent<Grid>();
        }
        for (int i = 0; i < _invenMaxSize; i++)
        {
            //if ()
            //grids[i].SetTile();
        }
    }
    void CheckTile()
    {
        for (int i = 0; i < _invenMaxSize; i++)
        {
            if ((grids[i].isActive == false && grids[i + 1].isActive == true) || (grids[i].isActive == false && grids[i - 1].isActive == true) || (grids[i].isActive == false && grids[i + 8] == true) || grids[i].isActive == false && grids[i - 8] == true)
            {
                grids[i].SetTile(1);
            }
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
