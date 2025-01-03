using UnityEngine;

public class StageController : MonoBehaviour
{
    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        var obj = PlayerManager.Instance.GetPlayer().transform.GetChild(1);
        var spawner = obj.GetComponent<EnemySpawner>();
        spawner.StartSpawning();
    }
}
