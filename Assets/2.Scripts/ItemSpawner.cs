using Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] Item;
    ItemName WeaponName;
    int itemRan;
    void Start()
    {
        itemRan = UnityEngine.Random.Range(0, 3);
        Temp(itemRan);
        Instantiate(Item[itemRan], transform);
        ItemInfo spawneItem = transform.GetChild(0).GetComponent<ItemInfo>();
        spawneItem.itemNumber = itemRan;


        spawneItem.itemName = "Axe";  // 임시
    }

    //void Start()
    //{
    //    itemRan = UnityEngine.Random.Range(0, 3);
    //    Temp(itemRan);

    //    // 아이템을 생성하고 생성된 오브젝트 참조 저장
    //    GameObject spawnedItem = Instantiate(Item[itemRan], transform);

    //    // 생성된 아이템의 ItemInfo 컴포넌트 가져오기
    //    ItemInfo spawnedItemInfo = spawnedItem.GetComponent<ItemInfo>();

    //    // 생성된 아이템의 itemNumber 설정
    //    if (spawnedItemInfo != null)
    //    {
    //        spawnedItemInfo.itemNumber = itemRan;
    //    }
    //    else
    //    {
    //        Debug.LogError("아이템 프리팹에 ItemInfo 컴포넌트가 없습니다!");
    //    }
    //}

    void Temp(int index)
    {
        // 임시
        if (itemRan == 0)
        {
            WeaponName = ItemName.Axe;
        }
        else if (itemRan == 1)
        {
            WeaponName = ItemName.HockeyStick;
        }
        else if (itemRan == 2)
        {
            WeaponName = ItemName.SniperRifle;
        }
    }
}
