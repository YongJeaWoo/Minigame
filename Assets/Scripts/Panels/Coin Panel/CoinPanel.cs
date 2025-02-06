using TMPro;
using UnityEngine;

public class CoinPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        InitSetCoinPanel();
    }

    private void InitSetCoinPanel()
    {
        var coin = PlayerManager.Instance.GetMyHasCoin();
        coinText.text = coin.ToString();
    }
}
