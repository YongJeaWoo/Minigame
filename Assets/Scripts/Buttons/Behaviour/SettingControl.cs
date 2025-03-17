using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject settingExitButton;

    public void ButtonsBehaviour()
    {
        string sceneName = SceneStateManager.Instance.CurrentScene;

        switch (sceneName)
        {
            case "Title":
                buttons[1].gameObject.SetActive(false);
                settingExitButton.SetActive(true);
                break;
            case "Game":
                buttons[1].gameObject.SetActive(true);
                settingExitButton.SetActive(false);
                break;
        }
    }
}
