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

    private Collider2D border;

    private void Start()
    {
        StageManager.Instance.OnStageEnd += StopSpawning;
    }

    private void OnDestroy()
    {
        StageManager.Instance.OnStageEnd -= StopSpawning;
    }

    public void StartSpawning()
    {
        if (spawnBoss) return;

        isSpawning = false;

        if (!isSpawning)
        {
            StartCoroutine(SpawnEnemiesCoroutine());
        }
    }

    public void SpawnBoss(int bossIndex)
    {
        int actualIndex = (bossIndex * 5) + 4;

        if (actualIndex < 0 || actualIndex >= enemiesPrefab.Length)
        {
            Debug.LogError("유효하지 않은 보스 몬스터 인덱스입니다.");
            return;
        }

        spawnBoss = true;
        isSpawning = false;
        StopAllCoroutines();
        StartCoroutine(SpawnBossCoroutine(actualIndex));
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        Debug.Log($"일반 적 호출");
        isSpawning = true;

        List<GameObject> normalEnemies = GetNormalEnemies();
        if (normalEnemies.Count == 0)
        {
            yield break;
        }

        while (isSpawning)
        {
            List<Transform> validSpawns = GetDynamicSpawnPositions();

            if (validSpawns.Count == 0)
            {
                Debug.LogWarning("스폰 가능한 위치가 없습니다.");
                yield break;
            }

            int spawnCount = Random.Range(1, validSpawns.Count + 1);

            for (int i = 0; i < spawnCount; i++)
            {
                if (validSpawns.Count == 0) break;

                GameObject randomEnemy = normalEnemies[Random.Range(0, normalEnemies.Count)];
                Transform spawnLocation = GetRandomSpawnLocationWithinBounds(validSpawns);

                if (spawnLocation != null)
                {
                    ObjectPoolManager.Instance.GetFromPool(randomEnemy, spawnLocation);
                }
            }

            yield return spawnInterval;
        }
    }

    private IEnumerator SpawnBossCoroutine(int bossIndex)
    {
        int adjustedIndex = bossIndex;

        if (adjustedIndex < 0 || adjustedIndex >= enemiesPrefab.Length)
        {
            Debug.LogError($"잘못된 인덱스 접근: {adjustedIndex}");
            yield break;
        }

        GameObject bossPrefab = enemiesPrefab[adjustedIndex];
        Debug.Log($"보스 프리팹: {bossPrefab.name}");

        Transform randomSpawn = GetRandomSpawnLocationWithinBounds(new List<Transform>(spawnsPos));
        if (randomSpawn != null)
        {
            Debug.Log($"보스를 이 위치에서 소환: {randomSpawn.position}");
            ObjectPoolManager.Instance.GetFromPool(bossPrefab, randomSpawn);
        }
        else
        {
            Debug.LogWarning("보스를 소환할 위치를 찾을 수 없습니다.");
        }

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

    private List<Transform> GetDynamicSpawnPositions()
    {
        List<Transform> dynamicSpawns = new();

        foreach (Transform spawn in spawnsPos)
        {
            if (IsWithinPlayableArea(spawn.position))
            {
                dynamicSpawns.Add(spawn);
            }
        }

        return dynamicSpawns;
    }

    private Transform GetRandomSpawnLocationWithinBounds(List<Transform> availableSpawns)
    {
        availableSpawns.RemoveAll(spawn => !IsWithinPlayableArea(spawn.position));
        if (availableSpawns.Count == 0)
        {
            Debug.LogWarning("스폰 가능한 위치가 없습니다.");
            return null;
        }

        int randomIndex = Random.Range(0, availableSpawns.Count);
        Transform selectedSpawn = availableSpawns[randomIndex];
        availableSpawns.RemoveAt(randomIndex);
        return selectedSpawn;
    }

    private bool IsWithinPlayableArea(Vector3 position)
    {
        if (border != null)
        {
            return border.bounds.Contains(position);
        }

        return false;
    }

    public Collider2D GetCollider(StageController stageController)
    {
        border = stageController.GetConfinerBorder();
        return border;
    }
}
