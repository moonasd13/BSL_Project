using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _uniqInstance;

    int _invenMaxSize = 64;
    [SerializeField]
    Grid[] grids;
    [SerializeField]
    public List<ItemInfo> InvenInitems { get; set; } = new List<ItemInfo>();

    [SerializeField]
    GameObject[] InGameItem;

    [SerializeField]
    Transform TempTargetPlay;

    [SerializeField]
    GameObject InvenUI;

    [SerializeField]    // 임시
    GameObject monsterSpawner;

    public static RectTransform _rootInvenTransform;

    public static InventoryManager _instance
    {
        get { return _uniqInstance; }
    }

    void Start()
    {
        monsterSpawner.SetActive(false);

        _uniqInstance = this;
        _rootInvenTransform = GameObject.Find("InventoryCanvas").GetComponent<RectTransform>();
        InitInvenSize();
    }
    void InitInvenSize()
    {
        grids = new Grid[_invenMaxSize];
        for (int i = 0; i < _invenMaxSize; i++)
        {
            grids[i] = transform.GetChild(0).GetChild(i).GetComponent<Grid>();
            //Debug.Log(grids[i].name);
        }
    }

    public void TestItem(ItemInfo item)
    {
        InvenInitems.Add(item);
        for (int i = 0; i < InvenInitems.Count; i++)
        {
            Debug.Log($"현재인벤아이템 : {InvenInitems[i].name}");
        }
    }
    public void GridSet(Grid[] grids)
    {
        for (int i = 0; i < grids.Length; i++)
        {
            //grids[i].isEmpty = false;
            grids[i].isEmpty = !grids[i].isEmpty;
        }
    }
    public void GridReset(Grid[] grids)
    {
        for (int i = 0; i < grids.Length; i++)
        {
            grids[i].isEmpty = true;
        }
    }
    public void SetItem(ItemInfo number)
    {
        Debug.Log(number.itemNumber);
        InvenInitems.Add(number);
    }
    public void SetItemStart()
    {
        for(int i = 0; i <InvenInitems.Count; i++)
        {
            GameObject obj = Instantiate(InGameItem[InvenInitems[i].itemNumber], TempTargetPlay);
            if (obj.transform.GetChild(0).GetComponent<OrbitAroundTarget>())
            {
                obj.transform.GetChild(0).GetComponent<OrbitAroundTarget>().index = i;
            }
        }
        //Instantiate(InGameItem[0], TempTargetPlay);
        InvenUI.SetActive(false);
        monsterSpawner.SetActive(true);
    }

    public void DebugItemInven()
    {
        Debug.Log("함수진입");
        for (int i = 0; i < InvenInitems.Count; i++)
        {
            Debug.Log(InvenInitems[i].itemNumber);
            Debug.Log(InvenInitems[i].itemName);
        }
    }
}
