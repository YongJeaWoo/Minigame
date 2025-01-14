using System.Collections.Generic;
using UnityEngine;

public class LevelupPanel : MonoBehaviour
{
    [SerializeField] private UpgradePanel[] panels;
    [SerializeField] private UpgradeList upgradeList;

    private void Start()
    {
        RandomSelectPanel();
    }

    private void RandomSelectPanel()
    {
        Time.timeScale = 0;
        List<UpgradeData> availableUpgrades = LevelDataManager.Instance.GetAvailableUpgrades(upgradeList.GetUpgradeData());

        if (availableUpgrades.Count < panels.Length) return;

        for (int i = 0; i < panels.Length; i++)
        {
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            panels[i].InitData(availableUpgrades[randomIndex]);
            availableUpgrades.RemoveAt(randomIndex);
        }
    }
}
