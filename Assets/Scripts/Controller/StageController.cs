using System.Collections;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("화면 경계 콜라이더")]
    [SerializeField] private Collider2D confinerBorder;

    private bool isCoroutineRunning = false;

    private void Start()
    {
        StartCoroutine(WaitSpawnEnemiesCoroutine());
    }

    private void SpawnEnemies()
    {
        var obj = PlayerManager.Instance.GetPlayer().transform.GetChild(1);
        var spawner = obj.GetComponent<EnemySpawner>();
        spawner.GetCollider(this);
        spawner.StartSpawning();
    }

    private IEnumerator WaitSpawnEnemiesCoroutine()
    {
        if (isCoroutineRunning) yield break;
        isCoroutineRunning = true;

        yield return StageManager.Instance.FadeMethod(1, 0);
        StageManager.Instance.StageStart();
        yield return new WaitForSeconds(1f);
        SpawnEnemies();

        isCoroutineRunning = false;
    }

    public Collider2D GetConfinerBorder() => confinerBorder;
}
