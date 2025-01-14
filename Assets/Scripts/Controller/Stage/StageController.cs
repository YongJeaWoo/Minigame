using Cinemachine;
using System.Collections;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("화면 경계 콜라이더")]
    [SerializeField] private Collider2D confinerBorder;
    [SerializeField] private CinemachineVirtualCamera cam;

    private GameObject player;

    private readonly string Levelup = $"Level Up Panel";

    private bool isCoroutineRunning = false;

    private void Start()
    {
        StageEntry();
    }

    private void StageEntry()
    {
        PlayerManager.Instance.InstantPlayer();
        player = PlayerManager.Instance.GetPlayer();
        cam.Follow = player.transform;
        cam.LookAt = player.transform;
        EventRegister(true);
        StartCoroutine(WaitSpawnEnemiesCoroutine());
    }

    private void OnDisable()
    {
        EventRegister(false);
    }

    private void EventRegister(bool isOn)
    {
        if (player == null) return;

        var playerExp = player.GetComponent<PlayerExp>();
        if (playerExp == null) return;

        if (isOn)
        {
            playerExp.OnLevelUp += LevelUpPopup;
        }
        else
        {
            playerExp.OnLevelUp -= LevelUpPopup;
        }

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

    private void SpawnEnemies()
    {
        var obj = PlayerManager.Instance.GetPlayer().transform.GetChild(1);
        var spawner = obj.GetComponent<EnemySpawner>();
        spawner.GetCollider(this);
        spawner.StartSpawning();
    }

    private void LevelUpPopup()
    {
        PopupManager.Instance.AddPopup(Levelup);
    }

    public Collider2D GetConfinerBorder() => confinerBorder;
}
