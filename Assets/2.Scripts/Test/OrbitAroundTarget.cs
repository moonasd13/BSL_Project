using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAroundTarget : MonoBehaviour
{
    public Transform target;      // 기준이 되는 게임오브젝트
    public float rotateSpeed = 50f; // 회전 속도 (도/초)
    public Vector3 rotateAxis = Vector3.up; // 회전 축

    private void Start()
    {
        target = transform.parent;
    }
    void Update()
    {
        if (target == null) return;

        transform.RotateAround(
            target.position,
            rotateAxis,
            rotateSpeed * Time.deltaTime
        );
    }

    //public Transform target;          // Player
    //public float rotateSpeed = 180f;  // 초당 공전 속도
    //public Vector3 orbitAxis = Vector3.up;

    //private Vector3 offset;

    //void Start()
    //{
    //    target = transform.parent;
    //    // Player 회전에 영향 안 받게
    //    transform.SetParent(null);

    //    offset = transform.position - target.position;
    //}

    //void LateUpdate()
    //{
    //    if (target == null) return;

    //    // 1? 오프셋 벡터 회전 (공전)
    //    offset = Quaternion.AngleAxis(
    //        rotateSpeed * Time.deltaTime,
    //        orbitAxis
    //    ) * offset;

    //    // 2? 위치 갱신
    //    transform.position = target.position + offset;

    //    // 3? 도끼날을 항상 "바깥쪽"으로 향하게
    //    Vector3 outwardDir = offset.normalized;
    //    transform.rotation = Quaternion.LookRotation(outwardDir, Vector3.up);
    //}
}
