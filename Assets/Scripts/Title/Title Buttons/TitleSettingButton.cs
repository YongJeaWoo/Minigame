public class TitleSettingButton : TitleCommonButton
{
    public override void ButtonClickedBehaviour()
    {
        var control = AudioManager.Instance.GetVolumeController();
        control.ToggleSetting();
    }
}
