using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class InventorySlotDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject draggingObj;     // 드래그중인 게임오브젝트
    GameObject draggingMesh;    // 드래그중인 게임오브젝트의 메쉬
    GameObject draggingObjPGrid;    // 드래그중인 오브젝트의 그리드를 가지고있게할 부모오브젝트
    GameObject[] draggingObjCGrid; // 드래그중인 오브젝트의 칸(Grid)
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
                Debug.Log("현재 회전값: " + rotateTest); // 직접 추적
                Debug.Log("쿼터니언: " + draggingObj.transform.localRotation.eulerAngles);

            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData) // 눌렀을때
    {
        if (draggingObj != null)
        {
            Destroy(draggingObj);
        }
        if (eventData.button != PointerEventData.InputButton.Left)  // 좌클릭으로만 가능하게
        {
            return;
        }
        

        item = gameObject.transform.GetComponent<ItemInfo>();  // 드래그한 아이템의 정보

        ItemInfo sourceItem = gameObject.transform.GetComponent<ItemInfo>();                              // 드래그한 오브젝트의 아이템 정보를 알기위함
        MeshFilter sourceFilter = gameObject.transform.GetChild(0).GetComponent<MeshFilter>();            // 드래그한 오브젝트의 메쉬필터 정보를 알기위함
        MeshRenderer sourceRenderer = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();      // 드래그한 오브젝트의 메쉬렌더 정보를 알기위함
        Transform sourceTransform = gameObject.transform.GetChild(0).GetComponent<Transform>();         // 드래그한 오브젝트의 Scale 및 Rotation 정보 알기위함

        Image[] sourceGrids = gameObject.transform.GetChild(1).GetComponentsInChildren<Image>();
        draggingObj = new GameObject("Dragging Item Object", typeof(RectTransform));                                                       // 드래그게임오브젝트생성          > Item_000
        draggingMesh = new GameObject("mesh", typeof(RectTransform));                                                                      // 드래그게임오브젝트의메쉬        > mesh

        draggingObjPGrid = new GameObject("Grids", typeof(RectTransform));                                                                  // 드래그게임오브젝트의 그리드칸을 가지고있는 부모 오브젝트
        draggingObjCGrid = new GameObject[gameObject.transform.GetChild(1).childCount];                  // 드래그게임오브젝트의그리드칸    > Grids   / 드래그한 오브젝트만큼 배열 초기화
        draggingObj.transform.SetParent(InventoryManager._rootInvenTransform.transform);        // InventoryCanvas의 자식으로 설정
        draggingObj.transform.SetAsLastSibling();                                               // InventoryCanvas의 마지막에 생성되도록 설정
        draggingObj.layer = InventoryManager._rootInvenTransform.gameObject.layer;              // 드래그중인 이미지를 ui에 띄우게 레이어를 InventoryCanvas로 설정
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
        //CanvasGroup canGroup = draggingObj.AddComponent<CanvasGroup>();                         // 드래그중인 오브젝트에 CanvasGroup 컴포넌트 추가
        //canGroup.blocksRaycasts = false;
        ItemInfo draggingItem = draggingObj.AddComponent<ItemInfo>();                           // 드래그중인 오브젝트에 ItemInfo 컴포넌트 추가
        MeshFilter draggingFilter = draggingMesh.AddComponent<MeshFilter>();                     // 드래그중인 오브젝트에 MeshFilter 컴포넌트 추가
        MeshRenderer draggingRenderer = draggingMesh.AddComponent<MeshRenderer>();               // 드래그중인 오브젝트에 MeshRenderer 컴포넌트 추가
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
    public void OnDrag(PointerEventData eventData)      // 드래그 중일때
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
    //public void OnEndDrag(PointerEventData eventData)   // 드래그가 끝났을때
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
    //        gameObject.transform.GetChild(0).gameObject.SetActive(true);       // 임시
    //        gameObject.transform.GetChild(1).gameObject.SetActive(true);       // 임시
    //        gameObject.SetActive(false);
    //        Debug.Log("fales");
    //    }

    //    Destroy(draggingObj);
    //}
    #endregion
    public void OnEndDrag(PointerEventData eventData)
    {
        if (emptyCheck.Availability)        // 조건1. 아이템칸들이 전부 비어 있는칸인가?
        {
            if (originalGrids != null && originalGrids.Length > 0)
            {
                Debug.Log("함수 진입");
                if (!IsSameGridPosition(originalGrids, testGrids))
                {
                    Debug.Log("위치 변경됨, GridSet 수행");
                    InventoryManager._instance.GridSet(testGrids);
                }
                else
                {
                    Debug.Log("같은 위치로 드래그함");
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
                    originalGrids[i] = emptyCheck.gridRayChecks[i].hitGrid; // 아이템을 다시 옮길때 그리드의 정보를 아이템도 가지게 해야할것
                }
                item.GetGrids(originalGrids);
                //originalGrids = item.curGrids();

                //tempitemInfo.GetGrids(testGrids);

                InventoryManager._instance.GridSet(originalGrids);

                Destroy(placedItem.GetComponentInChildren<EmptyCheck>());       // 레이체크스크립트 제거
                GridRayCheck[] gridRayChecks = placedItem.transform.GetChild(1).GetComponentsInChildren<GridRayCheck>();
                foreach (GridRayCheck check in gridRayChecks)       // 레이 제거
                {
                    Destroy(check);
                }

                Image[] itemGrids = placedItem.transform.GetChild(1).GetComponentsInChildren<Image>();
                for (int i = 0; i < itemGrids.Length; i++)
                {
                    itemGrids[i].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                }
                placedItem.transform.SetParent(putGrid.transform.parent); // 부모 맞추기
                placedItem.transform.localPosition = putGrid.transform.localPosition;
                placedItem.transform.localScale = Vector3.one;
                Debug.Log("아이템 배치 완료");
                Destroy(gameObject);
            }
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.SetActive(true);
            Debug.Log("배치 실패");
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

    //        // 아이템 생성
    //        GameObject placedItem = Instantiate(gameObject, InventoryManager._rootInvenTransform);
    //        placedItem.SetActive(true);
    //        placedItem.transform.GetChild(0).gameObject.SetActive(true);
    //        placedItem.transform.GetChild(1).gameObject.SetActive(true);

    //        // 위치 보정: localPosition을 맞추기 위해 anchoredPosition으로 조절
    //        RectTransform placedRT = placedItem.GetComponent<RectTransform>();
    //        placedRT.anchoredPosition = putGridRT.anchoredPosition;
    //        placedRT.localScale = Vector3.one;
    //        placedItem.transform.SetParent(InventoryManager._rootInvenTransform); // Canvas 내부로 다시 세팅

    //        Debug.Log("아이템 배치 성공");
    //    }
    //    else
    //    {
    //        // 배치 실패 시
    //        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    //        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    //        gameObject.SetActive(false);
    //        Debug.Log("배치 실패");
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
