using UnityEngine;

public enum E_PublicUpgradeType
{
    AttackPower,
    AttackSpeed,
    AttackRange,
    MovementSpeed
}

public abstract class PublicUpgradeData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private float[] publicUpgrades;
    [SerializeField] private E_PublicUpgradeType upgradeType;

    public E_PublicUpgradeType GetUpgradeType() =>upgradeType;
    public Sprite GetIcon() => icon;
    public void SetPublicUpgrades(float[] values) => publicUpgrades = values;
    public float[] GetPublicUpgrades() => publicUpgrades;
    public virtual void ApplyPublicUpgrade()
    {
        var player = PlayerManager.Instance.GetPlayer();
        var upgrade = player?.GetComponent<CoinUpgradeValue>();
        if (upgrade == null) return;

        float totalUpgradeValue = PlayerManager.Instance.GetUpgradeControl().GetUpgradeValue(upgradeType);

        ApplyUpgradeToPlayer(upgrade, totalUpgradeValue);
    }
    
    protected abstract void ApplyUpgradeToPlayer(CoinUpgradeValue upgrade, float totalValue);
}
