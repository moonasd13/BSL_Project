using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] Item;
    ItemName WeaponName;


    private int itemRan;
    void Start()
    {

        int enumCount = Enum.GetValues(typeof(eInitItems)).Length;
        itemRan = UnityEngine.Random.Range(0, enumCount);
        Instantiate(Item[itemRan], transform);
        ItemInfo spawneItem = transform.GetChild(0).GetComponent<ItemInfo>();
        spawneItem.itemNumber = itemRan;        // einitItems와 같은값


        //spawneItem.itemName = "Axe";  // 임시

        InitItem();
        itemRan = UnityEngine.Random.Range(0, Item.Length);
        Item[itemRan].SetActive(true);
    }

    void InitItem()
    {
        for (int i = 0; i < Item.Length; i++)
        {
            Instantiate(Item[i], transform);
            ItemInfo spawneItem = transform.GetChild(0).GetComponent<ItemInfo>();
            spawneItem.itemNumber = i;        // einitItems와 같은값
            Item[i].SetActive(false);
        }
    }
}
