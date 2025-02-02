using System.Collections.Generic;
using UnityEngine;

public class UpgradeDataControl : MonoBehaviour
{
    [SerializeField] private PublicUpgradeDataList publicUpgradeList;

    private Dictionary<E_PublicUpgradeType, float> accumulatedUpgrades = new Dictionary<E_PublicUpgradeType, float>();

    private void Start()
    {
        foreach (var upgrade in publicUpgradeList.upgradeList)
        {
            accumulatedUpgrades[upgrade.GetUpgradeType()] = 0f;
        }
    }

    public void SetUpgradeValue(E_PublicUpgradeType type, float value)
    {
        if (accumulatedUpgrades.ContainsKey(type))
        {
            accumulatedUpgrades[type] += value;

            ApplyPublicUpgrade(type);

            Debug.Log($"Upgrade {type} accumulated to {accumulatedUpgrades[type]}");
        }
        else
        {
            Debug.LogWarning($"SetUpgradeValue: Upgrade type {type} not found.");
        }
    }

    public float GetUpgradeValue(E_PublicUpgradeType type)
    {
        return accumulatedUpgrades.TryGetValue(type, out float value) ? value : 0f;
    }

    private void ApplyPublicUpgrade(E_PublicUpgradeType type)
    {
        foreach (var upgrade in publicUpgradeList.upgradeList)
        {
            if (upgrade.GetUpgradeType() == type)
            {
                upgrade.ApplyPublicUpgrade();
                break;
            }
        }
    }

    public PublicUpgradeDataList GetPublicUpgradeList() => publicUpgradeList;
}
