using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private Collider2D confinerBorder;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        var obj = PlayerManager.Instance.GetPlayer().transform.GetChild(1);
        var spawner = obj.GetComponent<EnemySpawner>();
        spawner.GetCollider(this);
        spawner.StartSpawning();
    }

    public Collider2D GetConfinerBorder() => confinerBorder;
}
