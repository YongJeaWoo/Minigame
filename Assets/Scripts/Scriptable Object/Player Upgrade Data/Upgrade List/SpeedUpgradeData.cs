using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgradeDataSOJ", menuName = "Upgrade SOJ/Speed Upgrade Data")]
public class SpeedUpgradeData : UpgradeData
{
    [SerializeField] private float[] speedMultipliers;

    public override void ApplyUpgrade()
    {
        var player = PlayerManager.Instance.GetPlayer();
        var movement = player.GetComponent<PlayerMovement>();
        if (movement == null) return;

        int level = LevelDataManager.Instance.GetLevel(this);
        float multiplier = GetValueForLevel(level);

        movement.SetMoveSpeed(multiplier);
    }

    public override float GetValueForLevel(int level)
    {
        if (level - 1>= speedMultipliers.Length) return speedMultipliers[speedMultipliers.Length - 1];
        return speedMultipliers[level - 1];
    }

    public override string GetExplainText()
    {
        int level = LevelDataManager.Instance.GetLevel(this);
        float multiplier = GetValueForLevel(level);

        string formattedValue = $"<color=#138EFF>{multiplier}%</color>";
        return string.Format(explainText, formattedValue);
    }
}
