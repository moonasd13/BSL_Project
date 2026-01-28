using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopItemSlot : MonoBehaviour
{
    [SerializeField]
    public Transform StopItemSlotTransform;

    void Start()
    {
        StopItemSlotTransform = transform;
    }
}
