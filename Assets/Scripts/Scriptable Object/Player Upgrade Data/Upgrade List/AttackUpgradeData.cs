using UnityEngine;

[CreateAssetMenu(fileName = "AttackUpgradeDataSOJ", menuName = "Upgrade SOJ/Attack Upgrade Data")]
public class AttackUpgradeData : UpgradeData
{
    [SerializeField] private float[] attackMultipliers;

    public override void ApplyUpgrade()
    {
        var player = PlayerManager.Instance.GetPlayer();
        var detectAttack = player.GetComponent<DetectAttackClass>();
        if (detectAttack == null) return;

        int level = LevelDataManager.Instance.GetLevel(this);
        float multiplier = GetValueForLevel(level);

        detectAttack.SetAttackPoint(multiplier);
    }

    public override float GetValueForLevel(int level)
    {
        if (level - 1>= attackMultipliers.Length) return attackMultipliers[attackMultipliers.Length - 1];
        return attackMultipliers[level - 1];
    }

    public override string GetExplainText()
    {
        int level = LevelDataManager.Instance.GetLevel(this);
        float multiplier = (GetValueForLevel(level)) * 100;

        string formattedValue = $"<color=#138EFF>{multiplier:F1}%</color>";
        return string.Format(explainText, formattedValue);
    }
}
