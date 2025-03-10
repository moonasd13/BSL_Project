using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StopItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Vector2 _draggingOffset = Vector2.zero;
    GameObject draggingObj;     // �巡������ ���ӿ�����Ʈ
    GameObject draggingMesh;    // �巡������ ���ӿ�����Ʈ�� �޽�
    GameObject draggingObjPGrid;    // �巡������ ������Ʈ�� �׸��带 �������ְ��� �θ������Ʈ
    GameObject[] draggingObjCGrid; // �巡������ ������Ʈ�� ĭ(Grid)
    [SerializeField]
    ItemInfo item;
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
        RectTransform sourceRTransform = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        Image[] sourceGrids = gameObject.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Image>();
        draggingObj = new GameObject("Dragging Item Object", typeof(RectTransform));                                                       // �巡�װ��ӿ�����Ʈ����          > Item_000
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        draggingMesh = new GameObject("mesh", typeof(RectTransform));                                                                      // �巡�װ��ӿ�����Ʈ�Ǹ޽�        > mesh
        draggingObjPGrid = new GameObject("Grids", typeof(RectTransform));                                                                  // �巡�װ��ӿ�����Ʈ�� �׸���ĭ�� �������ִ� �θ� ������Ʈ
        draggingObjCGrid = new GameObject[gameObject.transform.GetChild(0).GetChild(1).childCount];                  // �巡�װ��ӿ�����Ʈ�Ǳ׸���ĭ    > Grids   / �巡���� ������Ʈ��ŭ �迭 �ʱ�ȭ
        draggingObj.transform.SetParent(InventoryManager._rootInvenTransform.transform);        // InventoryCanvas�� �ڽ����� ����
        draggingObj.transform.SetAsLastSibling();                                               // InventoryCanvas�� �������� �����ǵ��� ����
        draggingObj.layer = InventoryManager._rootInvenTransform.gameObject.layer;              // �巡������ �̹����� ui�� ���� ���̾ InventoryCanvas�� ����
        draggingObj.transform.localScale = Vector3.one;
        draggingMesh.transform.SetParent(draggingObj.transform);
        draggingMesh.layer = InventoryManager._rootInvenTransform.gameObject.layer;
        draggingObjPGrid.transform.SetParent(draggingObj.transform);
        draggingObjPGrid.layer = InventoryManager._rootInvenTransform.gameObject.layer;
        draggingObjPGrid.transform.localScale = Vector3.one;
        Image[] draggingGridImage = new Image[draggingObjCGrid.Length];
        for (int i = 0; i < gameObject.transform.GetChild(0).GetChild(1).childCount; i++)
        {
            draggingObjCGrid[i] = new GameObject($"Gird{i}", typeof(RectTransform));
            draggingGridImage[i] = draggingObjCGrid[i].AddComponent<Image>();
            draggingGridImage[i].sprite = sourceGrids[i].sprite;
            //draggingGridImage[i].rectTransform.sizeDelta = sourceGrids[i].rectTransform.sizeDelta;
            draggingObjCGrid[i].transform.SetParent(draggingObjPGrid.transform);
            draggingGridImage[i].rectTransform.localScale = draggingObjPGrid.transform.localScale;
            draggingObjCGrid[i].transform.localPosition = sourceGrids[i].rectTransform.localPosition;
            //draggingGridImage[i].rectTransform.position = sourceGrids[i].rectTransform.localPosition;
        }

        //rt.sizeDelta = sourceRTransform.sizeDelta;
        //rt.localScale = sourceRTransform.lossyScale;
        draggingObj.AddComponent<CanvasRenderer>();
        draggingMesh.transform.localScale = sourceTransform.localScale;
        draggingMesh.transform.position = sourceTransform.localPosition;
        draggingMesh.transform.localEulerAngles = sourceTransform.localEulerAngles;
        CanvasGroup canGroup = draggingObj.AddComponent<CanvasGroup>();                         // �巡������ ������Ʈ�� CanvasGroup ������Ʈ �߰�
        canGroup.blocksRaycasts = false;
        ItemInfo draggingItem = draggingObj.AddComponent<ItemInfo>();                           // �巡������ ������Ʈ�� ItemInfo ������Ʈ �߰�
        MeshFilter draggingFilter = draggingMesh.AddComponent<MeshFilter>();                     // �巡������ ������Ʈ�� MeshFilter ������Ʈ �߰�
        MeshRenderer draggingRenderer = draggingMesh.AddComponent<MeshRenderer>();               // �巡������ ������Ʈ�� MeshRenderer ������Ʈ �߰�
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
            //// �巡�� ���� �������� ��ũ�� ��ǥ
            //Vector3 screenPos = eventData.position + _draggingOffset;
            //// ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            //Camera cam = eventData.pressEventCamera;
            //Vector3 newPos;
            //if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvasRt, screenPos, GetComponent<Camera>(), out newPos))
            //{
            //    // �巡�� ���� �������� ��ġ�� ���� ��ǥ�� ����
            //    draggingObj.transform.position = newPos;
            //    draggingObj.transform.rotation = _canvasRt.rotation;
            //}
            Vector2 newPos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRt, eventData.position, Camera.main, out newPos))
            {
                draggingObj.transform.localPosition = newPos;
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
    void Update()
    {
        if (draggingObj != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {

            }
        }
    }
}
