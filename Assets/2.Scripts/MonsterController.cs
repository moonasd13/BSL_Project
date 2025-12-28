using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    Transform TargetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        TargetPlayer = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = TargetPlayer.position - transform.position;
        Vector3 dirN = dir.normalized;
        transform.position = transform.position +  dirN * 3 * Time.deltaTime;
    }
}
