using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class QuarterView8DirectionMove : MonoBehaviour
{
    Animator _animator;
    public float moveSpeed = 5f;
    public float gravity = -9.8f;

    CharacterController controller;
    Transform cam;

    Vector3 velocity;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        Move();
        ApplyGravity();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A, D
        float v = Input.GetAxisRaw("Vertical");   // W, S

        // 입력 벡터
        Vector3 inputDir = new Vector3(h, 0f, v).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            // 카메라 기준 방향 계산
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * v + camRight * h;

            // 이동
            controller.Move(moveDir * moveSpeed * Time.deltaTime);

            // 캐릭터 회전 (이동 방향 바라보게)
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                Time.deltaTime * 10f
            );
        }
        if (h != 0 || v != 0)
        {
            _animator.SetBool("IsRun", true);
        }
        else
        {
            _animator.SetBool("IsRun", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("IsRoll");
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

