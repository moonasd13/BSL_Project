using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class StopItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject SlotObj;
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
        if (eventData.button != PointerEventData.InputButton.Left)  // ��Ŭ�����θ� �����ϰ�
        {
            return;
        }

        ItemInfo sourceItem = gameObject.transform.GetComponentInChildren<ItemInfo>();                              // �巡���� ������Ʈ�� ������ ������ �˱�����
        MeshFilter sourceFilter = gameObject.transform.GetChild(0).GetComponentInChildren<MeshFilter>();            // �巡���� ������Ʈ�� �޽����� ������ �˱�����
        MeshRenderer sourceRenderer = gameObject.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();      // �巡���� ������Ʈ�� �޽����� ������ �˱�����
        Transform sourceTransform = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Transform>();         // �巡���� ������Ʈ�� Scale �� Rotation ���� �˱�����
        RectTransform sourceRTransform = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        Image[] sourceGrids = gameObject.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Image>();
        draggingObj = new GameObject("Dragging Item Object", typeof(RectTransform));                                                       // �巡�װ��ӿ�����Ʈ����          > Item_000
        draggingMesh = new GameObject("mesh", typeof(RectTransform));                                                                      // �巡�װ��ӿ�����Ʈ�Ǹ޽�        > mesh
        RectTransform meshRt = gameObject.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
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
            draggingObjCGrid[i].transform.SetParent(draggingObjPGrid.transform);
            draggingGridImage[i].rectTransform.localScale = draggingObjPGrid.transform.localScale;
            draggingGridImage[i].rectTransform.sizeDelta = new Vector2(65, 65);
            draggingGridImage[i].type = Image.Type.Sliced;
            draggingGridImage[i].color = new Color(draggingGridImage[i].color.r, draggingGridImage[i].color.g, draggingGridImage[i].color.b, 0.5f);
            draggingObjCGrid[i].transform.localPosition = sourceGrids[i].rectTransform.localPosition;
            //Debug.Log(draggingGridImage[i].rectTransform.sizeDelta);
        }
        draggingObj.AddComponent<CanvasRenderer>();
        draggingMesh.transform.localScale = sourceTransform.localScale;
        draggingMesh.transform.localPosition = sourceTransform.localPosition;
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
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)      // �巡�� ���϶�
    {
        if (draggingObj != null)
        {
            Vector2 newPos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRt, eventData.position, eventData.enterEventCamera, out newPos))
            {
                draggingObj.transform.localPosition = newPos;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)   // �巡�װ� ��������
    {
        Destroy(draggingObj);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);    // �ӽ�

        eventData.pointerDrag.transform.GetComponent<ItemInfo>();
                
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
