using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class OrbitAroundTarget : MonoBehaviour
//{
//    public Transform target;      // 기준이 되는 게임오브젝트
//    public float rotateSpeed = 50f; // 회전 속도 (도/초)
//    public Vector3 rotateAxis = Vector3.up; // 회전 축

//    private void Start()
//    {
//        //target = transform.parent;
//        //transform.SetParent(null);
//        target = GameObject.Find("SkillManager").GetComponent<Transform>();
//    }
//    void Update()
//    {
//        if (target == null) return;

//        transform.RotateAround(
//            target.position,
//            rotateAxis,
//            rotateSpeed * Time.deltaTime
//        );
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Monster"))
//        {
//            Debug.Log("Hit");
//            Destroy(other.gameObject);
//        }
//    }
//}

using UnityEngine;

public class OrbitAroundTarget : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 180f;
    public Vector3 orbitAxis = Vector3.up;
    public Transform Test;
    public int index;

    private Vector3 offset;

    void Start()
    {
        Test = transform.parent;

        Test.transform.SetParent(null);

        // 부모 분리
        //transform.SetParent(null);

        target = GameObject.Find("SkillManager").GetComponent<Transform>();

        Test.parent = target;

        // 초기 오프셋 고정
        offset = transform.position - target.position;
        transform.Rotate(0, 90, -90);

        int invensize = InventoryManager._uniqInstance.InvenInitems.Count;
        if (invensize == 0)
            invensize = 1;
        float rotateAngle = index * (360 / invensize);

        offset = Quaternion.AngleAxis(
            rotateAngle,
            orbitAxis
        ) * offset;


        //Debug.Log(offset);



        // 항상 target 위치 기준
        transform.position = target.position + offset;
        //transform.rotation =  //Quaternion.Euler(0 , rotateSpeed * Time.deltaTime, 0);

        transform.Rotate(-rotateAngle, 0, 0);

    }

    void LateUpdate()
    {
        if (target == null) return;

        // 오프셋 자체를 회전
        offset = Quaternion.AngleAxis(
            rotateSpeed * Time.deltaTime,
            orbitAxis
        ) * offset;


        //Debug.Log(offset);



        // 항상 target 위치 기준
        transform.position = target.position + offset;
        //transform.rotation =  //Quaternion.Euler(0 , rotateSpeed * Time.deltaTime, 0);
        transform.Rotate(-rotateSpeed * Time.deltaTime , 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Destroy(other.gameObject);
        }
    }
}

