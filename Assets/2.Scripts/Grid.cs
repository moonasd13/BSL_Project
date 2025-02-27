using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
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
    public void OnBeginDrag(PointerEventData eventData) // ��������
    {

    }
    public void OnDrag(PointerEventData eventData)      // �巡�� ���϶�
    {

    }
    public void OnEndDrag(PointerEventData eventData)   // �巡�װ� ��������
    {

    }
    public void OnDrop(PointerEventData eventData)
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
