using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Grid : MonoBehaviour
{
    [SerializeField]
    Image Image;                                // �׸���ĭ�� �̹��� ������ ���� Ŭ��������
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    GameObject item;
    Color plusColor = new Color(248, 210, 85);  // (+)�̹��� ��
    public bool isEmpty { get; set; }       // ĭ�� ����ִ��� Ȯ���ϴ� ����
    public bool isActive { get; set; }                   // ĭ�� Ȱ��ȭ �Ǿ��ִ��� Ȯ���ϴ� ����
    public GameObject Item;
    void Start()
    {
        Image = gameObject.GetComponent<Image>();
    }
}
