using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridRayCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GraphicRaycaster gr;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 0.3f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10000f))
        {
            Grid CheckGrid = hit.collider.gameObject.GetComponent<Grid>();
            //CheckGrid.Test();
            if (CheckGrid.isEmpty == false)
            {

            }
        }
        
        var ped = new PointerEventData(null);
        
    }
}
