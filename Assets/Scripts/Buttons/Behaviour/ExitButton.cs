using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void ExitPanel()
    {
        var offPanel = panel.activeSelf;
        panel.SetActive(!offPanel);
    }
}
