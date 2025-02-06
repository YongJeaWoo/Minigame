using UnityEngine;
public class TitleButton : MonoBehaviour
{
    public void TitleButtonClick()
    {
        var controller = AudioManager.Instance.GetSettingControl();
        var control = controller.GetSettingButton();
        var button = control.GetComponent<SettingButton>();
        button.SetBehaviour();
        LoadingManager.LoadScene($"Title");
    }
}
