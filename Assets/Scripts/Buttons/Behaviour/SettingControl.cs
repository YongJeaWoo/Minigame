using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    [SerializeField] private GameObject settingButton;
    [SerializeField] private Button[] buttons;

    public void ToggleLoadButton(bool isOn)
    {
        settingButton.SetActive(isOn);
    }

    public void ButtonsBehaviour()
    {
        string sceneName = SceneStateManager.Instance.CurrentScene;

        switch (sceneName)
        {
            case "Title":
                buttons[1].gameObject.SetActive(false);
                break;
            case "Game":
                buttons[1].gameObject.SetActive(true);
                break;
        }
    }

    public GameObject GetSettingButton() => settingButton;
}
