using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class InventorySlotDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
    [SerializeField]
    float rotateTest;
    bool isDragging;
    [SerializeField]
    Grid[] originalGrids;   // 점유하고있는 그리드칸
    [SerializeField]
    Grid[] tempGrids;

    void Start()
    {
        item = GetComponent<ItemInfo>();
    }

    void Update()
    {
        if (draggingObj != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //rotateTest += 90f;
                rotateTest = (rotateTest + 90f) % 360f;
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

        isDragging = true;
        if (isDragging == true) // 드래그중일때
        {
            for (int i = 0; i < originalGrids.Length; i++)
            {
                originalGrids[i].isEmpty = true;
            }
        }

        rotateTest = gameObject.transform.localEulerAngles.z;
        //Debug.Log(rotateTest);

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
        draggingItem.GetComponent<ItemInfo>().itemNumber = sourceItem.itemNumber;
        draggingItem.GetComponent<ItemInfo>().itemName = sourceItem.itemName;
        MeshFilter draggingFilter = draggingMesh.AddComponent<MeshFilter>();                     // 드래그중인 오브젝트에 MeshFilter 컴포넌트 추가
        MeshRenderer draggingRenderer = draggingMesh.AddComponent<MeshRenderer>();               // 드래그중인 오브젝트에 MeshRenderer 컴포넌트 추가


        draggingItem = item;

        draggingFilter.mesh = sourceFilter.mesh;
        draggingRenderer.sharedMaterial = sourceRenderer.sharedMaterial;
        _canvasRt = InventoryManager._rootInvenTransform.transform as RectTransform;


        gameObject.transform.GetChild(0).gameObject.SetActive(false);       // 기존 자리에 있던 아이템 비활성화
        gameObject.transform.GetChild(1).gameObject.SetActive(false);       // ""
        emptyCheck = draggingObjPGrid.AddComponent<EmptyCheck>();

        draggingObj.transform.localRotation = gameObject.transform.localRotation;
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

    public void OnEndDrag(PointerEventData eventData)
    {
        Grid putGrid;

        if (emptyCheck.Availability)        // 조건1. 아이템칸들이 전부 비어 있는칸인가?
        {
            if (originalGrids != null && originalGrids.Length > 0)      // 상황 : 인벤토리칸에서 다른칸으로 옮길때
            {

                Debug.Log("함수 진입");
                putGrid = emptyCheck.rootGrids.GetComponent<Grid>();
                tempGrids = new Grid[gameObject.transform.GetChild(1).childCount];      // 아이템이 위치할 그리드
                for (int i = 0; i < emptyCheck.gridRayChecks.Length; i++)
                {
                    tempGrids[i] = emptyCheck.gridRayChecks[i].hitGrid; // 아이템을 다시 옮길때 그리드의 정보를 아이템도 가지게 해야할것
                }

                if (!IsSameGridPosition(originalGrids, tempGrids))      // 이전 위치와 다음 위치가 다를 경우 실행
                {
                    Debug.Log("위치 변경됨");
                    InventoryManager._instance.GridReset(originalGrids);
                    InventoryManager._instance.GridSet(tempGrids);
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.localPosition = putGrid.transform.localPosition;
                    gameObject.transform.localRotation = Quaternion.Euler(0, 0, rotateTest);

                    SetlocalRotation();

                    originalGrids = tempGrids;
                    item.GetGrids(originalGrids);
                    //Debug.Log($"testGrids is {(tempGrids == null ? "null" : $"length: {tempGrids.Length}")}");

                    tempGrids = null;

                    isDragging = false;
                }
                else if (IsSameGridPosition(originalGrids, tempGrids))
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.SetActive(true);
                    Debug.Log("위치 변경 없음");
                }
            }


            else
            {
                putGrid = emptyCheck.rootGrids.GetComponent<Grid>();

                GameObject placedItem = Instantiate(draggingObj, InventoryManager._rootInvenTransform);                
                placedItem.SetActive(true);
                placedItem.AddComponent<InventorySlotDropHandler>();
                placedItem.transform.GetChild(0).gameObject.SetActive(true);
                placedItem.transform.GetChild(1).gameObject.SetActive(true);
                InventorySlotDropHandler inventorySlotDropHandler = placedItem.GetComponent<InventorySlotDropHandler>();
                
                item = placedItem.transform.GetComponent<ItemInfo>();
                originalGrids = new Grid[gameObject.transform.GetChild(1).childCount];
                for (int i = 0; i < emptyCheck.gridRayChecks.Length; i++)       // 
                {
                    originalGrids[i] = emptyCheck.gridRayChecks[i].hitGrid; // 아이템을 다시 옮길때 그리드의 정보를 아이템도 가지게 해야할것
                }
                item.GetGrids(originalGrids);       // 점유하고 있는 칸을 아이템인포가 저장
                inventorySlotDropHandler.originalGrids = item.curGrids();


                InventoryManager._instance.GridSet(originalGrids);      // 점유한 그리드칸의 T/F 전환

                Destroy(placedItem.GetComponentInChildren<EmptyCheck>());       // 레이체크스크립트 제거
                GridRayCheck[] gridRayChecks = placedItem.transform.GetChild(1).GetComponentsInChildren<GridRayCheck>();
                foreach (GridRayCheck check in gridRayChecks)       // 레이 제거
                {
                    Destroy(check);
                }

                Image[] itemGrids = placedItem.transform.GetChild(1).GetComponentsInChildren<Image>();
                for (int i = 0; i < itemGrids.Length; i++)          // 점유한그리드칸을 연하게 표시
                {
                    itemGrids[i].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                }
                placedItem.transform.SetParent(putGrid.transform.parent); // 부모 맞추기
                placedItem.transform.localPosition = putGrid.transform.localPosition;
                placedItem.transform.localRotation = Quaternion.Euler(0, 0, rotateTest);
                
                SetlocalRotation();
                placedItem.transform.localScale = Vector3.one;
                Debug.Log("아이템 배치 완료");

                InventoryManager._instance.SetItem(item);

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
        //Debug.Log($"testGrids is {(testGrids == null ? "null" : $"length: {testGrids.Length}")}");
        Destroy(draggingObj);
    }
    private bool IsSameGridPosition(Grid[] a, Grid[] b)     // 기존에 있던 위치와 새로 바뀔 위치가 같은지 확인하는 함수
    {
        if (a.Length != b.Length) return false;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;     // 다르면 false 반환
        }
        return true;        // 같으면 true 반환
    }
    public void SetlocalRotation()
    {
        rotateTest = gameObject.transform.localEulerAngles.z;
    }
}
