using Cinemachine;
using System.Collections;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("화면 경계 콜라이더")]
    [SerializeField] private Collider2D confinerBorder;
    [SerializeField] private CinemachineVirtualCamera cam;

    [Space(15f)]
    [SerializeField] private AudioClip levelUpClip;
    [SerializeField] private AudioClip normalClip;
    [SerializeField] private AudioClip bossClip;

    private GameObject player;

    private readonly string Levelup = $"Level Up Panel";

    private bool isCoroutineRunning = false;

    private void Start()
    {
        StageEntry();
    }

    private void StageEntry()
    {
        AudioManager.Instance.PlayBGM(normalClip);
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
        AudioManager.Instance.LoadUIButtonToggle(true);

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
        AudioManager.Instance.PlaySFX(levelUpClip);
        PopupManager.Instance.AddPopup(Levelup);
    }

    public void BossSoundPlay()
    {
        AudioManager.Instance.StopBGM(0.3f, () =>
        {
            AudioManager.Instance.PlayBGM(bossClip);
        });
    }

    public Collider2D GetConfinerBorder() => confinerBorder;
}
