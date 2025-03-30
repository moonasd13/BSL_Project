using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Grid : MonoBehaviour
{
    Image Image;                                // �׸���ĭ�� �̹��� ������ ���� Ŭ��������
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    ItemInfo item;
    Color SpaecColor = new Color(1f, 1f, 1f, 0.5f);
    //[SerializeField]
    //Color plusColor = new Color(248, 210, 85);  // (+)�̹��� ��
    public bool isEmpty { get; set; }       // ĭ�� ����ִ��� Ȯ���ϴ� ����
    void Start()
    {
        Image = gameObject.GetComponent<Image>();
    }
    void SetGrid(ItemInfo item)
    {
        Image.color = SpaecColor;
        this.item = item;
        isEmpty = true;
    }
    void RayCastHitCheck()
    {

    }
    public void Test()
    {
        Debug.Log("�浹üũ");
        Image.color = Color.red;
    }
}
