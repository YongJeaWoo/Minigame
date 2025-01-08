using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    [Header("화면 경계 콜라이더")]
    [SerializeField] private Collider2D confinerBorder;
    [Header("화면 페이드 아웃")]
    [SerializeField] private Image fadeImage;

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
