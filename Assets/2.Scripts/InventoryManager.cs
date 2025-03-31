using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager _uniqInstance;

    int _invenMaxSize = 64;
    [SerializeField]
    Grid[] grids;
    List<ItemInfo> InvenInitems { get; set; } = new List<ItemInfo>();
    public static RectTransform _rootInvenTransform;

    public static InventoryManager _instance
    {
        get { return _uniqInstance; }
    }

    void Start()
    {
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

        }

    }
    public void TestItem(ItemInfo item)
    {
        InvenInitems.Add(item);

    }
}
