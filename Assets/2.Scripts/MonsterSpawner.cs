using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Spawn Area")]
    public float minSpawnDistance = 8f;
    public float maxSpawnDistance = 12f;

    [Header("Spawn Control")]
    public float spawnInterval = 1.5f;
    public int maxMonsterCount = 100;

    [Header("Monster Prefabs")]
    public GameObject[] monsterPrefabs;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float rayHeight = 20f;   // 레이 시작 높이

    int currentMonsterCount;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (currentMonsterCount < maxMonsterCount)
            {
                SpawnMonster();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnMonster()
    {
        if (player == null || monsterPrefabs.Length == 0) return;

        Vector3 spawnPos;
        if (!TryGetGroundSpawnPosition(out spawnPos))
            return;

        Instantiate(
            monsterPrefabs[Random.Range(0, monsterPrefabs.Length)],
            spawnPos,
            Quaternion.identity
        );

        currentMonsterCount++;
    }

    bool TryGetGroundSpawnPosition(out Vector3 spawnPos)
    {
        for (int i = 0; i < 10; i++) // 최대 10번 시도
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

            Vector3 dir = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
            Vector3 candidatePos = player.position + dir * distance;

            // 위에서 아래로 레이
            Vector3 rayStart = candidatePos + Vector3.up * rayHeight;

            if (Physics.Raycast(
                rayStart,
                Vector3.down,
                out RaycastHit hit,
                rayHeight * 2f,
                groundLayer
            ))
            {
                spawnPos = hit.point;
                return true;
            }
        }

        spawnPos = Vector3.zero;
        return false;
    }

    public void OnMonsterDead()
    {
        currentMonsterCount--;
    }
}
