using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private GameObject settingObject;

    [System.Serializable]
    private class VolumeControl 
    {
        public Slider slider;
        public TextMeshProUGUI percentText;
        public Action<float> SetVolumeAction;
    }

    [SerializeField] private VolumeControl masterVolumeControl;
    [SerializeField] private VolumeControl bgmVolumeControl;
    [SerializeField] private VolumeControl sfxVolumeControl;

    public bool IsSettingOpen { get; private set; }

    private void Start()
    {
        InitSliders();
    }

    public void ToggleSetting()
    {
        bool isActive = settingObject.activeSelf;
        settingObject.SetActive(!isActive);
        IsSettingOpen = !isActive;

        var controller = AudioManager.Instance.GetSettingControl();
        controller.ButtonsBehaviour();

        if (isActive)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    private void InitSliders()
    {
        SetupSlider(masterVolumeControl, AudioManager.Instance.MasterVolume, 
            value => AudioManager.Instance.MasterVolume = value);
        SetupSlider(bgmVolumeControl, AudioManager.Instance.BGMVolume, 
            value => AudioManager.Instance.BGMVolume = value);
        SetupSlider(sfxVolumeControl, AudioManager.Instance.SFXVolume, 
            value => AudioManager.Instance.SFXVolume = value);
    }

    private void SetupSlider(VolumeControl control, float initialValue, Action<float> setVolumeAction)
    {
        control.slider.value = initialValue;
        control.SetVolumeAction = setVolumeAction;
        
        control.slider.onValueChanged.AddListener(value =>
        {
            UpdateVolume(value, control);
        });

        UpdateVolume(initialValue, control);
    }

    private void UpdateVolume(float value, VolumeControl control)
    {
        control.percentText.text = $"{Mathf.RoundToInt(value * 100)}%";
        control.SetVolumeAction?.Invoke(value);
    }

    public GameObject GetSettingObject() => settingObject;
}
