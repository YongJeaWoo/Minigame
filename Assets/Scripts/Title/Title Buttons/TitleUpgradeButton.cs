using UnityEngine;

public class TitleUpgradeButton : TitleCommonButton
{
    [SerializeField] private GameObject publicUpgradePanel;

    public override void ButtonClickedBehaviour()
    {
        if (buttonClickSound != null)
        {
            AudioManager.Instance.PlaySFX(buttonClickSound);
        }
        bool canOpen = !publicUpgradePanel.activeSelf;
        publicUpgradePanel.SetActive(canOpen);
    }
}
