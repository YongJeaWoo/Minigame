using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("적 프리팹")]
    [SerializeField] private GameObject[] enemiesPrefab;
    [Header("적 생성 위치")]
    [SerializeField] private Transform[] spawnsPos;

    private bool isSpawning = false;
    private bool spawnBoss = false;

    private WaitForSeconds spawnInterval = new(5);

    public void StartSpawning()
    {
        isSpawning = false;

        if (!isSpawning && !spawnBoss)
        {
            StartCoroutine(SpawnEnemiesCoroutine());
        }
    }

    public void SpawnBoss(int bossIndex)
    {
        spawnBoss = true;
        isSpawning = false;
        StopAllCoroutines();
        StartCoroutine(SpawnBossCoroutine(bossIndex));
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        isSpawning = true;

        List<GameObject> normalEnemies = GetNormalEnemies();
        if (normalEnemies.Count == 0)
        {
            yield break;
        }

        while (isSpawning)
        {
            int maxSpawnCount = spawnsPos.Length;
            int minSpawnCount = Mathf.FloorToInt(maxSpawnCount / maxSpawnCount / 2f);
            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1);

            List<Transform> availableSpawns = new(spawnsPos);

            for (int i = 0; i < spawnCount; i++)
            {
                if (availableSpawns.Count == 0) break;

                GameObject randomEnemy = normalEnemies[Random.Range(0, normalEnemies.Count)];
                Transform randomSpawn = GetRandomSpawnLocation(availableSpawns);

                ObjectPoolManager.Instance.GetFromPool(randomEnemy, randomSpawn);
            }

            yield return spawnInterval;
        }
    }

    private IEnumerator SpawnBossCoroutine(int bossIndex)
    {
        if (bossIndex < 1 || bossIndex > enemiesPrefab.Length || bossIndex % 5 != 0)
        {
            Debug.LogError("유효하지 않은 보스 몬스터 인덱스입니다.");
            yield break;
        }

        GameObject bossPrefab = enemiesPrefab[bossIndex - 1]; 
        Transform randomSpawn = spawnsPos[Random.Range(0, spawnsPos.Length)]; 

        ObjectPoolManager.Instance.GetFromPool(bossPrefab, randomSpawn);

        spawnBoss = false;

        yield break; 
    }

    private List<GameObject> GetNormalEnemies()
    {
        List<GameObject> normalEnemies = new();
        for (int i = 0; i < enemiesPrefab.Length; i++)
        {
            if ((i + 1) % 5 != 0) 
            {
                normalEnemies.Add(enemiesPrefab[i]);
            }
        }
        return normalEnemies;
    }

    private Transform GetRandomSpawnLocation(List<Transform> availableSpawns)
    {
        int randomIndex = Random.Range(0, availableSpawns.Count);
        Transform selectedSpawn = availableSpawns[randomIndex];
        availableSpawns.RemoveAt(randomIndex);
        return selectedSpawn;
    }
}
