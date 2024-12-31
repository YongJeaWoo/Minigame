using System.Collections;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    private Coroutine blinkCoroutine;
    private WaitForSeconds blinkTimer = new(0.5f);

    [Header("현재 데이터 확인용")]
    [SerializeField] private StageData currentData;

    private float remainingTimer;

    // 나중에 제거
    [SerializeField] private TextMeshProUGUI timerText;

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
        remainingTimer = currentData.stageTimer;
        StartCoroutine(TimeLimitCoroutine());
    }

    private IEnumerator TimeLimitCoroutine()
    {
        float startTime = Time.time;
        float endTime = startTime + remainingTimer;

        while (Time.time < endTime)
        {
            remainingTimer = Mathf.Max(endTime - Time.time, 0);
            UpdateTimer();
            yield return null;
        }

        OnTimerEnd();
    }

    private void OnTimerEnd()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
            timerText.color = Color.white;
        }

        Debug.Log("타이머 종료");
    }

    private void UpdateTimer()
    {
        if (timerText != null)
        {
            if (remainingTimer <= 0)
            {
                timerText.text = "00:00";

                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                    timerText.color = Color.white;
                }
            }
            else
            {
                int minutes = Mathf.FloorToInt(remainingTimer / 60f);
                int seconds = Mathf.FloorToInt(remainingTimer % 60f);
                timerText.text = $"{minutes:D2}:{seconds:D2}";

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
            timerText.color = isRed ? Color.red : Color.white;
            isRed = !isRed;
            yield return blinkTimer;
        }

        timerText.color = Color.white;
    }
}
