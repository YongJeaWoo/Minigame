using UnityEngine;

[CreateAssetMenu(fileName = "MagnetUpgradeDataSOJ", menuName = "Upgrade SOJ/Magnet Upgrade Data")]
public class MagnetUpgradeData : UpgradeData
{
    [SerializeField] private float[] distances;

    public override void ApplyUpgrade()
    {
        var player = PlayerManager.Instance.GetPlayer();
        var getter = player.GetComponent<GetterItem>();

        int level = LevelDataManager.Instance.GetLevel(this);
        float value = GetValueForLevel(level);

        getter.SetAbsorbRange(value);
    }

    public override float GetValueForLevel(int level)
    {
        if (level - 1 >= distances.Length) return distances[distances.Length - 1];
        return distances[level - 1];
    }

    public override string GetExplainText()
    {
        int level = LevelDataManager.Instance.GetLevel(this);
        float multiplier = GetValueForLevel(level);

        string formattedValue = $"<color=#138EFF>{multiplier}</color>";
        return string.Format(explainText, formattedValue);
    }
}
