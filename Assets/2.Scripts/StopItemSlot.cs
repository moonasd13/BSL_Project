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
    GameObject draggingMesh;    // �巡������ ���ӿ�����Ʈ�� �޽�
    GameObject[] draggingObjGrid; // �巡������ ������Ʈ�� ĭ(Grid)
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
        ItemInfo sourceItem = gameObject.transform.GetComponentInChildren<ItemInfo>();                              // �巡���� ������Ʈ�� ������ ������ �˱�����
        MeshFilter sourceFilter = gameObject.transform.GetChild(0).GetComponentInChildren<MeshFilter>();            // �巡���� ������Ʈ�� �޽����� ������ �˱�����
        MeshRenderer sourceRenderer = gameObject.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();      // �巡���� ������Ʈ�� �޽����� ������ �˱�����
        Transform sourceTransform = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Transform>();         // �巡���� ������Ʈ�� Scale �� Rotation ���� �˱�����
        Image[] sourceGrids = gameObject.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Image>();
        draggingObj = new GameObject("Dragging Item Object");                                                       // �巡�װ��ӿ�����Ʈ����
        draggingMesh = new GameObject("mesh");
        draggingObjGrid = new GameObject[gameObject.transform.GetChild(0).GetChild(1).childCount];
        draggingObj.transform.SetParent(InventoryManager._rootInvenTransform.transform);        // InventoryCanvas�� �ڽ����� ����
        draggingObj.transform.SetAsLastSibling();                                               // InventoryCanvas�� �������� �����ǵ��� ����
        draggingObj.layer = InventoryManager._rootInvenTransform.gameObject.layer;              // �巡������ �̹����� ui�� ���� ���̾ InventoryCanvas�� ����
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
        CanvasGroup canGroup = draggingObj.AddComponent<CanvasGroup>();                         // �巡������ ������Ʈ�� CanvasGroup ������Ʈ �߰�
        canGroup.blocksRaycasts = false;
        ItemInfo draggingItem = draggingObj.AddComponent<ItemInfo>();                           // �巡������ ������Ʈ�� ItemInfo ������Ʈ �߰�
        MeshFilter draggingFilter = draggingObj.AddComponent<MeshFilter>();                     // �巡������ ������Ʈ�� MeshFilter ������Ʈ �߰�
        MeshRenderer draggingRenderer = draggingObj.AddComponent<MeshRenderer>();               // �巡������ ������Ʈ�� MeshRenderer ������Ʈ �߰�
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
