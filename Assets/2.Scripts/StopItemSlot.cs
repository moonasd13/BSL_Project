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
    GameObject draggingObj;     // �巡������ ���ӿ�����Ʈ
    [SerializeField]
    GameObject item;
    [SerializeField]
    RectTransform _canvasRt;
    public void OnBeginDrag(PointerEventData eventData) // ��������
    {
        if (draggingObj != null)
        {
            Destroy(draggingObj);
        }
        ItemInfo sourceItem = gameObject.transform.GetComponentInChildren<ItemInfo>();
        MeshFilter sourceFilter = gameObject.transform.GetChild(0).GetComponentInChildren<MeshFilter>();
        MeshRenderer sourceRenderer = gameObject.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
        Transform sourceTransform = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Transform>();
        draggingObj = new GameObject("Dragging Item Object");

        //draggingObj.transform.SetParent(InventoryManager._rootInvenTransform.transform);
        //draggingObj.transform.SetAsLastSibling();
        draggingObj.transform.localScale = sourceTransform.localScale;
        draggingObj.transform.localEulerAngles = sourceTransform.localEulerAngles;
        CanvasGroup canGroup = draggingObj.AddComponent<CanvasGroup>();
        canGroup.blocksRaycasts = false;
        ItemInfo draggingItem = draggingObj.AddComponent<ItemInfo>();
        MeshFilter draggingFilter = draggingObj.AddComponent<MeshFilter>();
        MeshRenderer draggingRenderer = draggingObj.AddComponent<MeshRenderer>();
        draggingItem.name = sourceItem.name;
        draggingFilter.mesh = sourceFilter.mesh;
        draggingRenderer.sharedMaterial = sourceRenderer.sharedMaterial;
        _canvasRt = InventoryManager._rootInvenTransform.transform as RectTransform;
        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)      // �巡�� ���϶�
    {
        if (draggingObj != null)
        {
            // �巡�� ���� �������� ��ũ�� ��ǥ
            Vector3 screenPos = eventData.position + _draggingOffset;
            // ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            Camera cam = eventData.pressEventCamera;
            Vector3 newPos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvasRt, screenPos, GetComponent<Camera>(), out newPos))
            {
                // �巡�� ���� �������� ��ġ�� ���� ��ǥ�� ����
                draggingObj.transform.position = newPos;
                draggingObj.transform.rotation = _canvasRt.rotation;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)   // �巡�װ� ��������
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
