public class TitleHelpButton : TitleCommonButton
{
    private readonly string HelpPanelName = $"Help Panel";

    public override void ButtonClickedBehaviour()
    {
        var instant = PopupManager.Instance.AddPopup(HelpPanelName);
    }
}
