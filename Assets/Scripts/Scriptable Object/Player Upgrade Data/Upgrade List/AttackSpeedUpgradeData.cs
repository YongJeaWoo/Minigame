using UnityEngine;

[CreateAssetMenu(fileName = "AttackSpeedUpgradeDataSOJ", menuName = "Upgrade SOJ/Attack Speed Upgrade Data")]
public class AttackSpeedUpgradeData : UpgradeData
{
    [SerializeField] private float[] delayReduces;

    public override void ApplyUpgrade()
    {
        var player = PlayerManager.Instance.GetPlayer();
        if (!player.TryGetComponent<DetectAttackClass>(out var attackSpeed)) return;

        int level = LevelDataManager.Instance.GetLevel(this);
        float reduce = GetValueForLevel(level);

        attackSpeed.SetAttackDelay(reduce);
    }

    public override float GetValueForLevel(int level)
    {
        if (level - 1 >= delayReduces.Length) return delayReduces[level - 1];
        return delayReduces[level - 1];
    }

    public override string GetExplainText()
    {
        int level = LevelDataManager.Instance.GetLevel(this);
        float finalValue = delayReduces[level];

        string formattedValue = $"<color=#138EFF>{finalValue:F1}%</color>";
        return string.Format(explainText, formattedValue);
    }
}
