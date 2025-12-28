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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("Hit");
        }
    }
}
