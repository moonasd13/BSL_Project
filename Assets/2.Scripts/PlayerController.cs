using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("IsRun", true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("IsRoll");
        }
    }
}
