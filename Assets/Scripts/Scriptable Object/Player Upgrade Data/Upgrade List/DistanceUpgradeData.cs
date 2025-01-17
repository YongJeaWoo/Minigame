using UnityEngine;

[CreateAssetMenu(fileName = "DistanceUpgradeDataSOJ", menuName = "Upgrade SOJ/Distance Upgrade Data")]
public class DistanceUpgradeData : UpgradeData
{
    [SerializeField] private float[] distances;

    public override void ApplyUpgrade()
    {
        var player = PlayerManager.Instance.GetPlayer();
        var distance = player.GetComponent<DetectAttackClass>();
        if (distance == null) return;

        int level = LevelDataManager.Instance.GetLevel(this);
        float addedRange = GetValueForLevel(level);

        distance.SetAttackRange(addedRange);
    }

    public override float GetValueForLevel(int level)
    {
        if (level - 1 >= distances.Length) return distances[distances.Length - 1];
        return distances[level - 1];
    }

    public override string GetExplainText()
    {
        int level = LevelDataManager.Instance.GetLevel(this);
        float finalValue = distances[level];

        string formattedValue = $"<color=#138EFF>{finalValue}</color>";
        return string.Format(explainText, formattedValue);
    }
}
