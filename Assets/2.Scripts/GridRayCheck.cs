using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridRayCheck : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {
        Image image = gameObject.GetComponent<Image>();
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 0.3f);     // 디버그확인용
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10000f))
        {
            Grid CheckGrid = hit.collider.gameObject.GetComponent<Grid>();
            //CheckGrid.Test();
            if (CheckGrid.isEmpty == false && hit.collider != null)
            {
                image.color = Color.red;
            }
            else
            {
                image.color = Color.green;
            }
            
        }
    }
}
