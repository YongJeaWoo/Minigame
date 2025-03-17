public class TitleHelpButton : TitleCommonButton
{
    private readonly string HelpPanelName = $"Help Panel";

    public override void ButtonClickedBehaviour()
    {
        if (buttonClickSound != null)
        {
            AudioManager.Instance.PlaySFX(buttonClickSound);
        }
        PopupManager.Instance.AddPopup(HelpPanelName);
    }
}
