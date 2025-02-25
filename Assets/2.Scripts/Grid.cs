using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour
{
    Image Image;

    void Start()
    {
        Image = gameObject.GetComponent<Image>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Image.color = Color.green;
        }
        else
        {
            Image.color = Color.white;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Image.color = Color.white;
        }
    }
}
