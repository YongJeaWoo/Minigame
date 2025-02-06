using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private GameObject player;
    private GameObject playerPrefab;
    private UpgradeDataControl upgradeControl;
    private CoinData coinData;

    #region Singleton
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

        DoAwake();
        DontDestroyOnLoad(gameObject);
    }
    private void DoAwake()
    {
        upgradeControl = GetComponent<UpgradeDataControl>();
        coinData = GetComponent<CoinData>();
        LoadCoin();
    }
    #endregion
    
    #region SetPlayer
    public void SetPlayer(PlayerObjectData player)
    {
        playerPrefab = player.GetPlayerPrefab();
    }

    public void InstantPlayer()
    {
        player = Instantiate(playerPrefab);

        ApplyUpgradeToPlayer(player);
    }

    public void ApplyUpgradeToPlayer(GameObject player)
    {
        if (player == null) return;

        var upgradeDataList = upgradeControl.GetPublicUpgradeList();

        foreach (var upgradeData in upgradeDataList.upgradeList)
        {
            float upgradeValue = upgradeControl.GetUpgradeValue(upgradeData.GetUpgradeType());

            if (upgradeValue > 0)
            {
                upgradeData.ApplyPublicUpgrade();
                Debug.Log($"Applying upgrade {upgradeData.GetUpgradeType()} with value {upgradeValue}");
            }
        }
    }
    #endregion

    #region Coin Data
    public void SaveCoin()
    {
        coinData.SaveCoin();
    }

    public void LoadCoin()
    {
        coinData.LoadCoin();
    }

    public void UpdateCoin(int amount)
    {
        coinData.UpdateCoin(amount);
    }

    public int GetMyHasCoin()
    {
        return coinData.GetMyCoin();
    }
    #endregion

    public UpgradeDataControl GetUpgradeControl() => upgradeControl;
    public GameObject GetPlayer() => player;
}
