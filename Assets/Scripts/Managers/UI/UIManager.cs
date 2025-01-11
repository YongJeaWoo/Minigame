using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;

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
            Destroy(Instance);
        }
    }
    #endregion

    [Header("남은 시간 표시")]
    [SerializeField] private TextMeshProUGUI leftTimerText;
    [Header("플레이어 경험치")]
    [SerializeField] private Image expImage;


    public Image GetExpImage() => expImage;
    public TextMeshProUGUI GetTimer() => leftTimerText;
    public string SetTimer(string value) => leftTimerText.text = value;
    public Color SetTimerColor(Color color) => leftTimerText.color = color;
}
