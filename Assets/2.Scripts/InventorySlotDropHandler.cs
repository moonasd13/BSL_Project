using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class InventorySlotDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject draggingObj;     // �巡������ ���ӿ�����Ʈ
    GameObject draggingMesh;    // �巡������ ���ӿ�����Ʈ�� �޽�
    GameObject draggingObjPGrid;    // �巡������ ������Ʈ�� �׸��带 �������ְ��� �θ������Ʈ
    GameObject[] draggingObjCGrid; // �巡������ ������Ʈ�� ĭ(Grid)
    [SerializeField]
    EmptyCheck emptyCheck;
    [SerializeField]
    RectTransform draggingRootRectTransform;
    [SerializeField]
    ItemInfo item;
    RectTransform _canvasRt;
    float rotateTest;

    [SerializeField]
    Grid[] originalGrids;
    [SerializeField]
    Grid[] testGrids;

    void Update()
    {
        if (draggingObj != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                rotateTest += 90f;
                draggingObj.transform.localRotation = Quaternion.Euler(0, 0, rotateTest);
                Debug.Log("���� ȸ����: " + rotateTest); // ���� ����
                Debug.Log("���ʹϾ�: " + draggingObj.transform.localRotation.eulerAngles);

            }
        }
    }
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
        

        item = gameObject.transform.GetComponent<ItemInfo>();  // �巡���� �������� ����

        ItemInfo sourceItem = gameObject.transform.GetComponent<ItemInfo>();                              // �巡���� ������Ʈ�� ������ ������ �˱�����
        MeshFilter sourceFilter = gameObject.transform.GetChild(0).GetComponent<MeshFilter>();            // �巡���� ������Ʈ�� �޽����� ������ �˱�����
        MeshRenderer sourceRenderer = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();      // �巡���� ������Ʈ�� �޽����� ������ �˱�����
        Transform sourceTransform = gameObject.transform.GetChild(0).GetComponent<Transform>();         // �巡���� ������Ʈ�� Scale �� Rotation ���� �˱�����

        Image[] sourceGrids = gameObject.transform.GetChild(1).GetComponentsInChildren<Image>();
        draggingObj = new GameObject("Dragging Item Object", typeof(RectTransform));                                                       // �巡�װ��ӿ�����Ʈ����          > Item_000
        draggingMesh = new GameObject("mesh", typeof(RectTransform));                                                                      // �巡�װ��ӿ�����Ʈ�Ǹ޽�        > mesh

        draggingObjPGrid = new GameObject("Grids", typeof(RectTransform));                                                                  // �巡�װ��ӿ�����Ʈ�� �׸���ĭ�� �������ִ� �θ� ������Ʈ
        draggingObjCGrid = new GameObject[gameObject.transform.GetChild(1).childCount];                  // �巡�װ��ӿ�����Ʈ�Ǳ׸���ĭ    > Grids   / �巡���� ������Ʈ��ŭ �迭 �ʱ�ȭ
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
        for (int i = 0; i < gameObject.transform.GetChild(1).childCount; i++)
        {
            draggingObjCGrid[i] = new GameObject($"Gird{i}", typeof(RectTransform));
            draggingGridImage[i] = draggingObjCGrid[i].AddComponent<Image>();
            draggingGridImage[i].sprite = sourceGrids[i].sprite;
            draggingObjCGrid[i].transform.SetParent(draggingObjPGrid.transform);
            draggingGridImage[i].rectTransform.localScale = draggingObjPGrid.transform.localScale;
            draggingGridImage[i].rectTransform.sizeDelta = new Vector2(65, 65);
            draggingGridImage[i].type = Image.Type.Sliced;
            draggingGridImage[i].color = new Color(draggingGridImage[i].color.r, draggingGridImage[i].color.g, draggingGridImage[i].color.b, 0.5f);
            draggingObjCGrid[i].AddComponent<GridRayCheck>();
            draggingObjCGrid[i].transform.localPosition = sourceGrids[i].rectTransform.localPosition;
            //Debug.Log(draggingGridImage[i].rectTransform.sizeDelta);
        }
        draggingRootRectTransform = draggingObjCGrid[0].GetComponent<RectTransform>();
        draggingObj.AddComponent<CanvasRenderer>();
        draggingMesh.transform.localScale = sourceTransform.localScale;
        draggingMesh.transform.localPosition = sourceTransform.localPosition;
        draggingMesh.transform.localEulerAngles = sourceTransform.localEulerAngles;
        //CanvasGroup canGroup = draggingObj.AddComponent<CanvasGroup>();                         // �巡������ ������Ʈ�� CanvasGroup ������Ʈ �߰�
        //canGroup.blocksRaycasts = false;
        ItemInfo draggingItem = draggingObj.AddComponent<ItemInfo>();                           // �巡������ ������Ʈ�� ItemInfo ������Ʈ �߰�
        MeshFilter draggingFilter = draggingMesh.AddComponent<MeshFilter>();                     // �巡������ ������Ʈ�� MeshFilter ������Ʈ �߰�
        MeshRenderer draggingRenderer = draggingMesh.AddComponent<MeshRenderer>();               // �巡������ ������Ʈ�� MeshRenderer ������Ʈ �߰�
        //draggingObj.name = "apple";

        draggingItem = item;

        draggingFilter.mesh = sourceFilter.mesh;
        draggingRenderer.sharedMaterial = sourceRenderer.sharedMaterial;
        _canvasRt = InventoryManager._rootInvenTransform.transform as RectTransform;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        emptyCheck = draggingObjPGrid.AddComponent<EmptyCheck>();

        //rotateTest = gameObject.transform.localRotation.z;
        draggingObj.transform.localRotation = gameObject.transform.localRotation;
        //Debug.Log(draggingObj.name);
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
    #region
    //public void OnEndDrag(PointerEventData eventData)   // �巡�װ� ��������
    //{
    //    if (emptyCheck.Availability == true)
    //    {
    //        Grid putGrid = emptyCheck.rootGrids.GetComponent<Grid>();
    //        draggingRootRectTransform = putGrid.transform.GetComponent<RectTransform>();

    //        ItemInfo draggingInfo = eventData.pointerDrag.transform.GetComponent<ItemInfo>();
    //        InventoryManager._instance.TestItem(draggingInfo);
    //        draggingRootRectTransform = null;
    //        Debug.Log("true");
    //    }
    //    else
    //    {
    //        gameObject.transform.GetChild(0).gameObject.SetActive(true);       // �ӽ�
    //        gameObject.transform.GetChild(1).gameObject.SetActive(true);       // �ӽ�
    //        gameObject.SetActive(false);
    //        Debug.Log("fales");
    //    }

    //    Destroy(draggingObj);
    //}
    #endregion
    public void OnEndDrag(PointerEventData eventData)
    {
        if (emptyCheck.Availability)        // ����1. ������ĭ���� ���� ��� �ִ�ĭ�ΰ�?
        {
            if (originalGrids != null && originalGrids.Length > 0)
            {
                Debug.Log("�Լ� ����");
                if (!IsSameGridPosition(originalGrids, testGrids))
                {
                    Debug.Log("��ġ �����, GridSet ����");
                    InventoryManager._instance.GridSet(testGrids);
                }
                else
                {
                    Debug.Log("���� ��ġ�� �巡����");
                }
            }


            else
            {
                Grid putGrid = emptyCheck.rootGrids.GetComponent<Grid>();



                GameObject placedItem = Instantiate(draggingObj, InventoryManager._rootInvenTransform);
                placedItem.SetActive(true);
                placedItem.AddComponent<InventorySlotDropHandler>();
                placedItem.transform.GetChild(0).gameObject.SetActive(true);
                placedItem.transform.GetChild(1).gameObject.SetActive(true);
                //ItemInfo tempitemInfo = placedItem.transform.GetComponent<ItemInfo>();
                item = placedItem.transform.GetComponent<ItemInfo>();
                originalGrids = new Grid[gameObject.transform.GetChild(1).childCount];
                for (int i = 0; i < emptyCheck.gridRayChecks.Length; i++)       // 
                {
                    originalGrids[i] = emptyCheck.gridRayChecks[i].hitGrid; // �������� �ٽ� �ű涧 �׸����� ������ �����۵� ������ �ؾ��Ұ�
                }
                item.GetGrids(originalGrids);
                //originalGrids = item.curGrids();

                //tempitemInfo.GetGrids(testGrids);

                InventoryManager._instance.GridSet(originalGrids);

                Destroy(placedItem.GetComponentInChildren<EmptyCheck>());       // ����üũ��ũ��Ʈ ����
                GridRayCheck[] gridRayChecks = placedItem.transform.GetChild(1).GetComponentsInChildren<GridRayCheck>();
                foreach (GridRayCheck check in gridRayChecks)       // ���� ����
                {
                    Destroy(check);
                }

                Image[] itemGrids = placedItem.transform.GetChild(1).GetComponentsInChildren<Image>();
                for (int i = 0; i < itemGrids.Length; i++)
                {
                    itemGrids[i].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                }
                placedItem.transform.SetParent(putGrid.transform.parent); // �θ� ���߱�
                placedItem.transform.localPosition = putGrid.transform.localPosition;
                placedItem.transform.localScale = Vector3.one;
                Debug.Log("������ ��ġ �Ϸ�");
                Destroy(gameObject);
            }
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.SetActive(true);
            Debug.Log("��ġ ����");
        }
        Debug.Log($"testGrids is {(testGrids == null ? "null" : $"length: {testGrids.Length}")}");
        Destroy(draggingObj);
    }
    #region
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Destroy(draggingObj);

    //    if (emptyCheck.Availability)
    //    {
    //        Grid putGrid = emptyCheck.rootGrids.GetComponent<Grid>();
    //        RectTransform putGridRT = putGrid.GetComponent<RectTransform>();

    //        // ������ ����
    //        GameObject placedItem = Instantiate(gameObject, InventoryManager._rootInvenTransform);
    //        placedItem.SetActive(true);
    //        placedItem.transform.GetChild(0).gameObject.SetActive(true);
    //        placedItem.transform.GetChild(1).gameObject.SetActive(true);

    //        // ��ġ ����: localPosition�� ���߱� ���� anchoredPosition���� ����
    //        RectTransform placedRT = placedItem.GetComponent<RectTransform>();
    //        placedRT.anchoredPosition = putGridRT.anchoredPosition;
    //        placedRT.localScale = Vector3.one;
    //        placedItem.transform.SetParent(InventoryManager._rootInvenTransform); // Canvas ���η� �ٽ� ����

    //        Debug.Log("������ ��ġ ����");
    //    }
    //    else
    //    {
    //        // ��ġ ���� ��
    //        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    //        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    //        gameObject.SetActive(false);
    //        Debug.Log("��ġ ����");
    //    }
    //}
    #endregion
    private bool IsSameGridPosition(Grid[] a, Grid[] b)
    {
        if (a.Length != b.Length) return false;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
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
