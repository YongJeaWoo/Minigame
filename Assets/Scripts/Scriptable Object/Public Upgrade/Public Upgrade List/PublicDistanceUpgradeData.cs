using UnityEngine;

[CreateAssetMenu(fileName = "PublicDistanceUpgradeData", menuName = "Public Upgrade SOJ/Public Distance Upgrade Data")]
public class PublicDistanceUpgradeData : PublicUpgradeData
{
    protected override void ApplyUpgradeToPlayer(CoinUpgradeValue upgrade, float totalValue)
    {
        upgrade.SetAttackRangeUpgrade(totalValue);
    }
}
