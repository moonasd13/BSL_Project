using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public float attackCooldown = 0.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    float timer;

    // 감지된 몬스터 목록
    HashSet<Transform> monsters = new HashSet<Transform>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackCooldown)
        {
            Transform target = GetClosestMonster();

            if (target != null)
            {
                Fire(target);
                timer = 0f;
            }
        }
    }

    void Fire(Transform target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(target);
    }

    Transform GetClosestMonster()
    {
        float minDist = float.MaxValue;
        Transform closest = null;

        foreach (var monster in monsters)
        {
            if (monster == null) continue;

            float dist = Vector3.Distance(transform.position, monster.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = monster;
            }
        }

        return closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            monsters.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            monsters.Remove(other.transform);
        }
    }
}
