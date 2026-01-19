using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    Vector3 direction;

    public void Init(Transform target)
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        direction = (target.position - transform.position).normalized;
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
