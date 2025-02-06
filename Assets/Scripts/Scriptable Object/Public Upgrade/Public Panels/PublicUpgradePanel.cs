using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PublicUpgradePanel : MonoBehaviour
{
    [SerializeField] private Image[] publicUpgradeIcons;
    [SerializeField] private Button[] upgradeButtons;
    [SerializeField] private TextMeshProUGUI[] publicUpgradeTexts;

    [Header("업그레이드")]
    [SerializeField] private int[] upgradeCosts;

    private PublicUpgradeData publicUpgradeData;

    private int currentButtonIndex = 0;
    private readonly string AutoInfoPanel = $"Auto Info Panel";
    private readonly string NotCost = $"코인이 부족합니다!";

    public void SetUpgradeData(PublicUpgradeData data)
    {
        publicUpgradeData = data;
        InitData();
    }

    private void InitData()
    {
        for (int i = 0; i < publicUpgradeIcons.Length; i++)
        {
            if (i < publicUpgradeData.GetPublicUpgrades().Length)
            {
                publicUpgradeIcons[i].sprite = publicUpgradeData.GetIcon();
                publicUpgradeTexts[i].text = $"비용 : {upgradeCosts[i]}\n수치 : {publicUpgradeData.GetPublicUpgrades()[i]}";

                int index = i;
                upgradeButtons[i].onClick.AddListener(() => TryApplyUpgrade(index));
                UpdateButtonState(i);
            }
        }
    }

    private void TryApplyUpgrade(int index)
    {
        int playerCoins = PlayerManager.Instance.GetMyHasCoin();
        int cost = upgradeCosts[index];

        if (playerCoins >= cost)
        {
            ApplyUpgrade(index);
        }
        else
        {
            var autoPanel = PopupManager.Instance.AddPopup(AutoInfoPanel);
            var panel = autoPanel.GetComponentInChildren<IsolatePanel>();
            panel.SetInfoText(NotCost);
        }
    }

    private void ApplyUpgrade(int index)
    {
        if (index < publicUpgradeData.GetPublicUpgrades().Length)
        {
            int cost = upgradeCosts[index];

            PlayerManager.Instance.UpdateCoin(-cost);
            PlayerManager.Instance.SaveCoin();

            float upgradeValue = publicUpgradeData.GetPublicUpgrades()[index];
            PlayerManager.Instance.GetUpgradeControl().SetUpgradeValue(publicUpgradeData.GetUpgradeType(), upgradeValue);
            ActivateNextButton(index);
        }
    }

    private void UpdateButtonState(int index)
    {
        int playerCoins = PlayerManager.Instance.GetMyHasCoin();
        upgradeButtons[index].interactable = playerCoins >= upgradeCosts[index];
    }

    private void ActivateNextButton(int index)
    {
        if (index == currentButtonIndex)
        {
            if (currentButtonIndex < upgradeButtons.Length - 1)
            {
                currentButtonIndex++;
                upgradeButtons[currentButtonIndex].interactable = true;
            }
        }
    }
}
