using UnityEngine;

[CreateAssetMenu(fileName = "PublicAttackSpeedUpgradeData", menuName = "Public Upgrade SOJ/Public Attack Speed Upgrade Data")]
public class PublicAttackSpeedUpgradeData : PublicUpgradeData
{
    protected override void ApplyUpgradeToPlayer(CoinUpgradeValue upgrade, float totalValue)
    {
        upgrade.SetAttackDelayUpgrade(totalValue);
    }
}
