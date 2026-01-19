using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Grid : MonoBehaviour
{
    Image Image;
    [SerializeField]
    Sprite[] sprites;
    Color SpaecColor = new Color(1f, 1f, 1f, 0.5f);
    public bool isEmpty { get; set; }       // 칸이 비어있는지 확인하는 변수
    void Start()
    {
        isEmpty = true;
        Image = gameObject.GetComponent<Image>();
    }
}
