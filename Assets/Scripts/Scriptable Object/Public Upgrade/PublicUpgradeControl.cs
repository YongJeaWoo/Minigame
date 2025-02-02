using UnityEngine;

public class PublicUpgradeControl : MonoBehaviour
{
    [SerializeField] private PublicUpgradePanel[] publicUpgradePanels;
    private PublicUpgradeDataList publicUpgradeDataList;

    private void Start()
    {
        InitUpgradePanels();
    }

    private void InitUpgradePanels()
    {
        publicUpgradeDataList = PlayerManager.Instance.GetUpgradeControl().GetPublicUpgradeList();

        for (int i = 0; i < publicUpgradePanels.Length; i++)
        {
            if (i < publicUpgradeDataList.upgradeList.Count)
            {
                publicUpgradePanels[i].SetUpgradeData(publicUpgradeDataList.upgradeList[i]);
            }
        }
    }
}
