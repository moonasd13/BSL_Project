using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
using Define;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _uniqInstance;


    public Dictionary<eInitItems, ItemType> initItems = new Dictionary<eInitItems, ItemType>();


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

    [SerializeField]
    Transform storeRoot;

    [SerializeField]
    StopItemSlot[] stopItemSlots;

    [SerializeField]
    ItemSpawner itemSpawner;


    public static RectTransform _rootInvenTransform;

    public static InventoryManager _instance
    {
        get { return _uniqInstance; }
    }

    void Start()
    {

        initItems.Add(eInitItems.Axe, ItemType.Weapon);
        initItems.Add(eInitItems.HockeyStick, ItemType.Weapon);
        initItems.Add(eInitItems.SniperRifle, ItemType.Weapon);
        initItems.Add(eInitItems.Hpcharm, ItemType.Stats);
        initItems.Add(eInitItems.Speedcharm, ItemType.Stats);

        //stopItemSlots = storeRoot.GetComponentsInChildren<StopItemSlot>();

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
            // invenInitems의 i에의해서 속성에 따라 소환을 결정 => 무기, 스탯

            if (initItems.TryGetValue((eInitItems)InvenInitems[i].itemNumber, out Define.ItemType val))
            {
                if (ItemType.Weapon == val)
                {
                    GameObject obj = Instantiate(InGameItem[InvenInitems[i].itemNumber], TempTargetPlay);
                    if (obj.transform.GetChild(0).GetComponent<OrbitAroundTarget>())
                    {
                        obj.transform.GetChild(0).GetComponent<OrbitAroundTarget>().index = i;
                    }
                }
            }
        }
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
    public void ItemSale(int reroll)
    {

    }
    public void itemReroll()
    {

    }    
}
