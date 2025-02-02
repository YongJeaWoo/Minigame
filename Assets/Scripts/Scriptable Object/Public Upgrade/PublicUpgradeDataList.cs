using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PublicUpgradeDataList", menuName = "Public Upgrade List SOJ/Public Upgrade Data List")]
public class PublicUpgradeDataList : ScriptableObject
{
    public List<PublicUpgradeData> upgradeList;
}
