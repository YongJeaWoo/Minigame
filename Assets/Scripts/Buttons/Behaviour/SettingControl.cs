using UnityEngine;

public class SettingControl : MonoBehaviour
{
    [SerializeField] private GameObject buttonCanvas;

    public void ToggleLoadButton(bool isOn)
    {
        buttonCanvas.SetActive(isOn);
    }
}
