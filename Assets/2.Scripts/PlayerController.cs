using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    CharacterController _characterController;
    float _speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    _animator.SetBool("IsRun", true);
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    _animator.SetTrigger("IsRoll");
        //}
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(x, 0, y);
            _characterController.Move(dir * _speed * Time.deltaTime);
        if (x != 0 || y != 0)
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
}
