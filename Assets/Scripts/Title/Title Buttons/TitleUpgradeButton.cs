using UnityEngine;

public class TitleUpgradeButton : TitleCommonButton
{
    [SerializeField] private GameObject publicUpgradePanel;

    public override void ButtonClickedBehaviour()
    {
        bool canOpen = !publicUpgradePanel.activeSelf;
        publicUpgradePanel.SetActive(canOpen);
    }
}
