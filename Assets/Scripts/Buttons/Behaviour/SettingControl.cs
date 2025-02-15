using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

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
}
