using UnityEngine;

[CreateAssetMenu(fileName = "PublicAttackUpgradeData", menuName = "Public Upgrade SOJ/Public Attack Upgrade Data")]
public class PublicAttackUpgradeData : PublicUpgradeData
{
    protected override void ApplyUpgradeToPlayer(CoinUpgradeValue upgrade, float totalValue)
    {
        upgrade.SetAttackPointUpgrade(totalValue);
    }
}
