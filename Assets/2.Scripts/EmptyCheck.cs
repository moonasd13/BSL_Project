using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCheck : MonoBehaviour
{
    public bool Availability { get; private set; }
    [SerializeField]
    //GridRayCheck[] gridRayChecks;       // 아이템을 놔둘 기준그리드
    public GridRayCheck[] gridRayChecks { get; private set; }
    [SerializeField]
    public Grid rootGrids;
    [SerializeField]
    public Grid[] grids;
    void Start()
    {
        gridRayChecks = new GridRayCheck[gameObject.transform.childCount];
        grids = new Grid[gameObject.transform.childCount];
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
                grids[i] = gridRayChecks[i].hitGrid;
            }
        }
        rootGrids = gridRayChecks[0].hitGrid;
    }
}
