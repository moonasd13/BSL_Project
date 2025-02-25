using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour
{
    Image Image;
    [SerializeField]
    Sprite[] sprites;
    Color plusColor = new Color(248, 210, 85);  // +이미지 색
    bool isEmpty;       // 칸이 비어있는지 확인하는 변수
    void Start()
    {
        Image = gameObject.GetComponent<Image>();
    }
    #region
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Item"))
    //    {
    //        Image.color = Color.green;
    //    }
    //    else
    //    {
    //        Image.color = Color.white;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Item"))
    //    {
    //        Image.color = Color.white;
    //    }
    //}
    #endregion

    public void SetTile()
    {

    }
}
