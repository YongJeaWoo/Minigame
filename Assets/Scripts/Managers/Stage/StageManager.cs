using System;
using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    #region Singleton 
    public static StageManager Instance;

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public event Action OnStageEnd;

    private Coroutine blinkCoroutine;
    private WaitForSeconds blinkTimer = new(0.5f);

    [Header("현재 데이터 확인용")]
    [SerializeField] private StageData currentData;

    private PlayerHealth playerHealth;

    private float remainingTimer;
    private bool isStageEnd;

    private readonly string panelName = $"Info Panel";

    public void SetStageData(StageData data)
    {
        currentData = data;
        remainingTimer = currentData.stageTimer;
        UpdateTimer(); 
    }

    private void Start()
    {
        StageStart();
    }

    public void StageStart()
    {
        isStageEnd = false;
        remainingTimer = currentData.stageTimer;
        playerHealth = PlayerManager.Instance.GetPlayer().GetComponent<PlayerHealth>();
        StartCoroutine(TimeLimitCoroutine());
    }

    private IEnumerator TimeLimitCoroutine()
    {
        float startTime = Time.time;
        float endTime = startTime + remainingTimer;

        while (Time.time < endTime)
        {
            if (playerHealth.GetIsDead())
            {
                OnPlayerDeadEnd();
                yield break;
            }

            remainingTimer = Mathf.Max(endTime - Time.time, 0);
            UpdateTimer();
            yield return null;
        }

        OnTimerEnd();
    }

    private void OnPlayerDeadEnd()
    {
        StopCoroutine(TimeLimitCoroutine());
        OnTimerEnd();
    }

    private void OnTimerEnd()
    {
        OnStageEnd?.Invoke();

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
            UIManager.Instance.SetTimerColor(Color.white);
        }

        isStageEnd = true;

        var panel = PopupManager.Instance.AddPopup(panelName);

        if (playerHealth.GetIsDead())
        {
            var info = panel.GetComponentInChildren<InfoPanel>();
            info.SetInfoText($"플레이어가 죽었습니다.");
        }
        else
        {
            var info = panel.GetComponentInChildren<InfoPanel>();
            info.SetInfoText($"게임을 클리어 했습니다.");
        }
    }

    private void UpdateTimer()
    {
        if (UIManager.Instance.GetTimer() != null)
        {
            if (remainingTimer <= 0)
            {
                UIManager.Instance.SetTimer($"00:00");

                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                    UIManager.Instance.SetTimerColor(Color.white);
                }
            }
            else
            {
                int minutes = Mathf.FloorToInt(remainingTimer / 60f);
                int seconds = Mathf.FloorToInt(remainingTimer % 60f);
                UIManager.Instance.SetTimer($"{minutes:D2}:{seconds:D2}");

                if (remainingTimer <= 10)
                {
                    if (blinkCoroutine == null)
                    {
                        blinkCoroutine = StartCoroutine(BlinkTextCoroutine());
                    }
                }
            }
        }
    }

    private IEnumerator BlinkTextCoroutine()
    {
        bool isRed = true;

        while (remainingTimer <= 10 && remainingTimer > 0)
        {
            UIManager.Instance.SetTimerColor(isRed ? Color.red : Color.white);
            isRed = !isRed;
            yield return blinkTimer;
        }

        UIManager.Instance.SetTimerColor(Color.white);
    }

    public bool GetIsStageEnd() => isStageEnd;
}
