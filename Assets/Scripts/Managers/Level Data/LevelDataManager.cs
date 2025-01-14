using System.Collections.Generic;
using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    public static LevelDataManager Instance;

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

    private Dictionary<UpgradeData, int> upgradeLevels = new();

    private const int maxLevel = 5;

    public int GetLevel(UpgradeData upgradeData)
    {
        if (upgradeLevels.ContainsKey(upgradeData))
        {
            return upgradeLevels[upgradeData];
        }

        return 0; 
    }

    public void LevelUp(UpgradeData upgradeData)
    {
        if (!upgradeLevels.ContainsKey(upgradeData))
        {
            upgradeLevels[upgradeData] = 0;  
        }

        int currentLevel = upgradeLevels[upgradeData];

        if (currentLevel < maxLevel)
        {
            upgradeLevels[upgradeData] = currentLevel + 1; 
        }
    }

    public List<UpgradeData> GetAvailableUpgrades(List<UpgradeData> allUpgrades)
    {
        List<UpgradeData> availableUpgrades = new();
        foreach (var upgrade in allUpgrades)
        {
            if (GetLevel(upgrade) < maxLevel)
            {
                availableUpgrades.Add(upgrade);
            }
        }
        return availableUpgrades;
    }
}
