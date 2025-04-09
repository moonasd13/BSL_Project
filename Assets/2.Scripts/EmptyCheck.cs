using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCheck : MonoBehaviour
{
    public bool Availability { get; private set; }
    [SerializeField]
    GridRayCheck[] gridRayChecks;       // �������� ���� ���ر׸���
    [SerializeField]
    public Grid rootGrids;
    void Start()
    {
        gridRayChecks = new GridRayCheck[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gridRayChecks[i] = transform.GetChild(i).GetComponent<GridRayCheck>();
        }
    }
    void Update()
    {
        for (int i = 0; i < gridRayChecks.Length; i++)
        {
            if (gridRayChecks[i].isEmpty == false)
            {
                Availability = false;
                break;
            }
            else
            {
                Availability = true;
            }
        }
        rootGrids = gridRayChecks[0].hitGrid;
    }
}
