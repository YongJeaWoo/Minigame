public class SettingButton : BehaviourButton
{
    public override void SetBehaviour()
    {
        var control = AudioManager.Instance.GetVolumeController();
        control.ToggleSetting();
        var isActive = control.IsSettingOpen;

        if (isActive)
        {
            iconImage.sprite = xIcon;
        }
        else
        {
            iconImage.sprite = behaviourIcon;
        }
    }
}
