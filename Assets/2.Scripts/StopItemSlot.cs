using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StopItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Vector2 _draggingOffset = Vector2.zero;
    [SerializeField]
    GameObject draggingObj;     // 드래그중인 게임오브젝트
    GameObject draggingMesh;    // 드래그중인 게임오브젝트의 메쉬
    GameObject[] draggingObjGrid; // 드래그중인 오브젝트의 칸(Grid)
    [SerializeField]
    GameObject item;
    [SerializeField]
    RectTransform _canvasRt;
    public void OnBeginDrag(PointerEventData eventData) // 눌렀을때
    {
        if (draggingObj != null)
        {
            Destroy(draggingObj);
        }
        ItemInfo sourceItem = gameObject.transform.GetComponentInChildren<ItemInfo>();                              // 드래그한 오브젝트의 아이템 정보를 알기위함
        MeshFilter sourceFilter = gameObject.transform.GetChild(0).GetComponentInChildren<MeshFilter>();            // 드래그한 오브젝트의 메쉬필터 정보를 알기위함
        MeshRenderer sourceRenderer = gameObject.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();      // 드래그한 오브젝트의 메쉬렌더 정보를 알기위함
        Transform sourceTransform = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Transform>();         // 드래그한 오브젝트의 Scale 및 Rotation 정보 알기위함
        Image[] sourceGrids = gameObject.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Image>();
        draggingObj = new GameObject("Dragging Item Object");                                                       // 드래그게임오브젝트생성
        draggingMesh = new GameObject("mesh");
        draggingObjGrid = new GameObject[gameObject.transform.GetChild(0).GetChild(1).childCount];
        draggingObj.transform.SetParent(InventoryManager._rootInvenTransform.transform);        // InventoryCanvas의 자식으로 설정
        draggingObj.transform.SetAsLastSibling();                                               // InventoryCanvas의 마지막에 생성되도록 설정
        draggingObj.layer = InventoryManager._rootInvenTransform.gameObject.layer;              // 드래그중인 이미지를 ui에 띄우게 레이어를 InventoryCanvas로 설정
        Image[] draggingGridImage = new Image[draggingObjGrid.Length];
        for (int i = 0; i < gameObject.transform.GetChild(0).GetChild(1).childCount; i++)
        {
            draggingObjGrid[i] = new GameObject($"Gird{i}");
            draggingGridImage[i] = draggingObjGrid[i].AddComponent<Image>();
            draggingGridImage[i].sprite = sourceGrids[i].sprite;
            draggingGridImage[i].rectTransform.sizeDelta = sourceGrids[i].rectTransform.sizeDelta;
            draggingGridImage[i].rectTransform.position = sourceGrids[i].rectTransform.localPosition;
            draggingObjGrid[i].transform.SetParent(draggingObj.transform);
        }

        //draggingObj.transform.SetLocalPositionAndRotation(sourceTransform.transform.forward, sourceTransform.rotation);
        //draggingObj.transform.localScale = sourceTransform.localScale;
        //draggingObj.transform.localEulerAngles = sourceTransform.localEulerAngles;
        CanvasGroup canGroup = draggingObj.AddComponent<CanvasGroup>();                         // 드래그중인 오브젝트에 CanvasGroup 컴포넌트 추가
        canGroup.blocksRaycasts = false;
        ItemInfo draggingItem = draggingObj.AddComponent<ItemInfo>();                           // 드래그중인 오브젝트에 ItemInfo 컴포넌트 추가
        MeshFilter draggingFilter = draggingObj.AddComponent<MeshFilter>();                     // 드래그중인 오브젝트에 MeshFilter 컴포넌트 추가
        MeshRenderer draggingRenderer = draggingObj.AddComponent<MeshRenderer>();               // 드래그중인 오브젝트에 MeshRenderer 컴포넌트 추가
        draggingItem.name = sourceItem.name;
        draggingFilter.mesh = sourceFilter.mesh;
        draggingRenderer.sharedMaterial = sourceRenderer.sharedMaterial;
        _canvasRt = InventoryManager._rootInvenTransform.transform as RectTransform;
        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)      // 드래그 중일때
    {
        if (draggingObj != null)
        {
            // 드래그 중인 아이콘의 스크린 좌표
            Vector3 screenPos = eventData.position + _draggingOffset;
            // 스크린 좌표를 월드 좌표로 변환
            Camera cam = eventData.pressEventCamera;
            Vector3 newPos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvasRt, screenPos, GetComponent<Camera>(), out newPos))
            {
                // 드래그 중인 아이콘의 위치를 월드 좌표로 설정
                draggingObj.transform.position = newPos;
                draggingObj.transform.rotation = _canvasRt.rotation;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)   // 드래그가 끝났을때
    {
        Destroy(draggingObj);
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
