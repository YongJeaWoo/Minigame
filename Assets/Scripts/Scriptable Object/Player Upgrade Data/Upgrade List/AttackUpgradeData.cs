using UnityEngine;

[CreateAssetMenu(fileName = "AttackUpgradeDataSOJ", menuName = "Upgrade SO/Attack Upgrade Data")]
public class AttackUpgradeData : UpgradeData
{
    [SerializeField] private float[] attackMultipliers;

    public override int GetCountForLevel(int level)
    {
        return 0;
    }

    public override float GetValueForLevel(int level)
    {
        if (level - 1 >= attackMultipliers.Length) return attackMultipliers[attackMultipliers.Length - 1];
        return attackMultipliers[level - 1];
    }

    public override void ApplyUpgrade()
    {
        var player = PlayerManager.Instance.GetPlayer();
        var detectAttack = player.GetComponent<DetectAttackClass>();
        if (detectAttack == null) return;

        int level = LevelDataManager.Instance.GetLevel(this);
        float multiplier = GetValueForLevel(level);

        detectAttack.SetAttackPoint(multiplier);
    }
}
