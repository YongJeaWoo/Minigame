using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeListSOJ")]
public class UpgradeList : ScriptableObject
{
    [SerializeField] private List<UpgradeData> upgradesData;

    public List<UpgradeData> GetUpgradeData() => upgradesData;
}
