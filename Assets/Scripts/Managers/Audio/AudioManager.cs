using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    #region Singleton
    private void Awake()
    {
        Singleton();
        DoAwake();
    }

    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [SerializeField] private VolumeController volumeController;
    [SerializeField] private SettingControl settingControl;

    private Dictionary<string, AudioChannel> audioChannels = new Dictionary<string, AudioChannel>();

    private string BgmVolumeString = $"BGM";
    private string SfxVolumeString = $"SFX";

    private float masterVolume = 0.5f;
    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

    public float MasterVolume
    {
        get => masterVolume;
        set
        {
            masterVolume = Mathf.Clamp01(value);
            UpdateChannelVolumes();
        }
    }

    public float BGMVolume
    {
        get => bgmVolume;
        set
        {
            bgmVolume = Mathf.Clamp01(value);
            UpdateChannelVolumes();
        }
    }

    public float SFXVolume
    {
        get => sfxVolume;
        set
        {
            sfxVolume = Mathf.Clamp01(value);
            UpdateChannelVolumes();
        }
    }

    private void UpdateChannelVolumes()
    {
        if (audioChannels.ContainsKey(BgmVolumeString))
        {
            audioChannels[BgmVolumeString].SetVolume(masterVolume * bgmVolume);
        }
        if (audioChannels.ContainsKey(SfxVolumeString))
        {
            audioChannels[SfxVolumeString].SetVolume(masterVolume * sfxVolume);
        }
    }

    private void DoAwake()
    {
        audioChannels[BgmVolumeString] = new AudioChannel(transform, "BGM_Channel", 1);
        audioChannels[SfxVolumeString] = new AudioChannel(transform, "SFX_Channel", 10);
    }

    public void PlayBGM(AudioClip clip, bool loop = true, float volume = 0.5f, float fadeDuration = 0.5f)
    {
        var channel = audioChannels[BgmVolumeString];
        channel.Play(clip, loop, volume * BGMVolume, fadeDuration);
    }

    public void StopBGM(float fadeDuration = 0.5f, Action onComplete = null)
    {
        var channel = audioChannels[BgmVolumeString];
        if (fadeDuration > 0f)
        {
            channel.Stop(fadeDuration, onComplete);
        }
        else
        {
            channel.Stop();
            onComplete?.Invoke();
        }
    }

    public void PlaySFX(AudioClip clip, bool loop = false, float volume = 0.5f)
    {
        var channel = audioChannels[SfxVolumeString];
        channel.Play(clip, loop, volume * SFXVolume);
    }

    public void StopSFX(AudioClip clip)
    {
        var channel = audioChannels[SfxVolumeString];
        channel.Stop(clip);
    }
    
    public void LoadUIButtonToggle(bool isOn)
    {
        settingControl.ToggleLoadButton(isOn);
    }

    public SettingControl GetSettingControl() => settingControl;
    public VolumeController GetVolumeController() => volumeController;
}
