using UnityEngine;

public class UpgradePanelButton : BehaviourButton
{
    [SerializeField] private GameObject publicUpgradePanel;

    protected override void Start()
    {
        base.Start();
    }

    public override void SetBehaviour()
    {
        bool canOpen = !publicUpgradePanel.activeSelf;
        publicUpgradePanel.SetActive(canOpen);
    }
}
