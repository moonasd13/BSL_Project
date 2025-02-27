using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    int _invenMaxSize = 64;
    [SerializeField]
    Grid[] grids;
    [SerializeField]
    GameObject _draggingObject;
    [SerializeField]
    RectTransform _rootInvenTransform;

    void Start()
    {
        _rootInvenTransform = GameObject.Find("InventoryCanvas").GetComponent<RectTransform>();
        InitInvenSize();
        //CheckTile();
    }
    void InitInvenSize()
    {
        grids = new Grid[_invenMaxSize];
        for (int i = 0; i < _invenMaxSize; i++)
        {
            grids[i] = transform.GetChild(0).GetChild(i).GetComponent<Grid>();
            Debug.Log(grids[i].name);
            grids[i].SetTile(1);
        }
    }
    void CheckTile()
    {
        for (int i = 0; i < _invenMaxSize; i++)
        {
            if ((grids[i].isActive == false && grids[i + 1].isActive == true) ||
                (grids[i].isActive == false && grids[i - 1].isActive == true) ||
                (grids[i].isActive == false && grids[i + 8].isActive == true) ||
                (grids[i].isActive == false && grids[i - 8] == true))
            {
                grids[i].SetTile(1);
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData) // 눌렀을때
    {
        GameObject storePos = GameObject.Find("Store");
        MeshFilter meshFilter = storePos.transform.GetChild(0).GetChild(0).GetComponentInChildren<MeshFilter>();
        MeshRenderer meshRenderer = storePos.transform.GetChild(0).GetChild(0).GetComponentInChildren<MeshRenderer>();
        _draggingObject = new GameObject("Dragging Item Object");
        _draggingObject.transform.SetParent(_rootInvenTransform.transform);
        _draggingObject.transform.SetAsLastSibling();
        _draggingObject.transform.localScale = Vector3.one;
        CanvasGroup canGroup = _draggingObject.AddComponent<CanvasGroup>();
        canGroup.blocksRaycasts = false;
        MeshFilter draggingMF = _draggingObject.AddComponent<MeshFilter>();
        MeshRenderer draggingMR = _draggingObject.AddComponent<MeshRenderer>();
        draggingMF.mesh = meshFilter.mesh;
        draggingMR.material = meshRenderer.material;
        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)      // 드래그 중일때
    {

    }
    public void OnEndDrag(PointerEventData eventData)   // 드래그가 끝났을때
    {
        Destroy(_draggingObject);
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
