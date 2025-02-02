using UnityEngine;

[CreateAssetMenu(fileName = "PublicSpeedUpgradeData", menuName = "Public Upgrade SOJ/Public Speed Upgrade Data")]
public class PublicSpeedUpgradeData : PublicUpgradeData
{
    protected override void ApplyUpgradeToPlayer(CoinUpgradeValue upgrade, float totalValue)
    {
        upgrade.SetMovementUpgrade(totalValue);
    }
}
