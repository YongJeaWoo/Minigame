using UnityEngine;

public class CoinUpgradeValue : MonoBehaviour
{
    private float attackPointUpgrade;
    private float attackRangeUpgrade;
    private float attackDelayUpgrade;
    private float movementUpgrade;
    
    public float GetAttackPointUpgrade() => attackPointUpgrade;
    public void SetAttackPointUpgrade(float value) => attackPointUpgrade = value;

    public float GetAttackRangeUpgrade() => attackRangeUpgrade;
    public void SetAttackRangeUpgrade(float value) => attackRangeUpgrade = value;

    public float GetAttackDelayUpgrade() => attackDelayUpgrade;
    public void SetAttackDelayUpgrade(float value) => attackDelayUpgrade = value;
    public float GetMovementUpgrade() => movementUpgrade;
    public void SetMovementUpgrade(float value) => movementUpgrade = value;
}
