using UnityEngine;

[CreateAssetMenu(fileName = "DetectedUpgradeDataSOJ", menuName = "Upgrade SOJ/Detected Count Upgrade Data")]
public class DetectedCountUpgradeData : UpgradeData
{
    [SerializeField] private int[] counts;

    public override void ApplyUpgrade()
    {
        var player = PlayerManager.Instance.GetPlayer();
        if (!player.TryGetComponent<DetectAttackClass>(out var count)) return;

        int level = LevelDataManager.Instance.GetLevel(this);
        float floatValue = GetValueForLevel(level);

        var finalAddValue = Mathf.RoundToInt(floatValue);

        count.IncreaseDetectedTargets(this, finalAddValue, counts.Length);
    }

    public override float GetValueForLevel(int level)
    {
        if (level - 1 >= counts.Length) return counts[level - 1];
        return counts[level - 1];
    }

    public override string GetExplainText()
    {
        int level = LevelDataManager.Instance.GetLevel(this);
        float finalValue = counts[level];

        string formattedValue = $"<color=#138EFF>{finalValue}</color>";
        return string.Format(explainText, formattedValue);
    }
}
