using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PublicUpgradePanel : MonoBehaviour
{
    [SerializeField] private Image[] publicUpgradeIcons;
    [SerializeField] private Button[] upgradeButtons;
    [SerializeField] private TextMeshProUGUI[] publicUpgradeTexts;

    private PublicUpgradeData publicUpgradeData;

    private int currentButtonIndex = 0;

    public void SetUpgradeData(PublicUpgradeData data)
    {
        publicUpgradeData = data;
        InitData();
    }

    private void InitData()
    {
        for (int i = 1; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].interactable = false;
        }

        for (int i = 0; i < publicUpgradeIcons.Length; i++)
        {
            if (i < publicUpgradeData.GetPublicUpgrades().Length)
            {
                publicUpgradeIcons[i].sprite = publicUpgradeData.GetIcon();
                publicUpgradeTexts[i].text = publicUpgradeData.GetPublicUpgrades()[i].ToString();

                int index = i;
                upgradeButtons[i].onClick.AddListener(() => ApplyUpgrade(index));
            }
        }
    }

    private void ApplyUpgrade(int index)
    {
        if (index < publicUpgradeData.GetPublicUpgrades().Length)
        {
            float upgradeValue = publicUpgradeData.GetPublicUpgrades()[index];
            PlayerManager.Instance.GetUpgradeControl().SetUpgradeValue(publicUpgradeData.GetUpgradeType(), upgradeValue);
            ActivateNextButton(index);
        }
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
