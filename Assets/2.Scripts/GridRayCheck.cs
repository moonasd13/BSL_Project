using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridRayCheck : MonoBehaviour
{
    public bool isEmpty { get; private set; }
    public Grid hitGrid { get; private set; }

    Image image;
    Color defaultColor;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        defaultColor = image.color;
    }

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 0.3f);     // �����Ȯ�ο�
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10000f))
        {
            Grid CheckGrid = hit.collider.gameObject.GetComponent<Grid>();
            if (CheckGrid.isEmpty == true && hit.collider.gameObject.CompareTag("InventoryGrid"))
            {
                isEmpty = true;
                image.color = Color.green;
                hitGrid = hit.collider.gameObject.GetComponent<Grid>();     //0��°Grid���� ���̸������� �浹�� �κ�Grid
            }
            else
            {
                isEmpty = false;
                image.color = Color.red;
            }
        }
        else
        {
            isEmpty = false;
            image.color = defaultColor;
        }
    }
}
