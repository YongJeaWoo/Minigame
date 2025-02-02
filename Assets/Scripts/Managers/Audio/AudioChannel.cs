using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChannel
{
    private Transform parentTransform;
    private List<AudioSource> sources = new List<AudioSource>();
    private int maxSources;

    public AudioChannel(Transform parent, string name, int maxSources)
    {
        parentTransform = parent;
        this.maxSources = maxSources;

        if (maxSources == 1)
        {
            CreateNewSource();
        }
    }

    private AudioSource CreateNewSource()
    {
        GameObject audioObject = new($"{parentTransform.name}_AudioSource");
        audioObject.transform.SetParent(parentTransform);
        AudioSource source = audioObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        sources.Add(source);
        return source;
    }

    public void Play(AudioClip clip, bool loop, float volume, float fadeDuration = 0f)
    {
        if (clip == null)
        {
            Debug.LogError($"클립이 존재하지 않음");
            return;
        }

        AudioSource source = GetAvailableSource();
        if (fadeDuration > 0f)
        {
            source.volume = 0f;
            source.clip = clip;
            source.loop = loop;
            source.Play();
            FadeVolume(source, volume, fadeDuration);
        }
        else
        {
            source.clip = clip;
            source.loop = loop;
            source.volume = volume;
            source.Play();
        }
    }

    public void Stop(AudioClip clip)
    {
        var source = sources.Find(s => s.clip == clip && s.isPlaying);
        if (source != null)
        {
            source.Stop();
        }
    }

    public void Stop(float fadeDuration = 0f, Action onComplete = null)
    {
        foreach (var source in sources)
        {
            if (fadeDuration > 0f)
            {
                FadeVolume(source, 0f, fadeDuration, () =>
                {
                    source.Stop(); 
                    onComplete?.Invoke();
                });
            }
            else
            {
                source.Stop();
                onComplete?.Invoke();
            }
        }
    }

    public void SetVolume(float volume)
    {
        foreach (var source in sources)
        {
            source.volume = volume;
        }
    }

    private void FadeVolume(AudioSource source, float targetVolume, float duration, Action onComplete = null)
    {
        AudioManager.Instance.StartCoroutine(FadeCoroutine(source, targetVolume, duration, onComplete));
    }

    private IEnumerator FadeCoroutine(AudioSource source, float targetVolume, float duration, Action onComplete)
    {
        float startVolume = source.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        source.volume = targetVolume;
        onComplete?.Invoke();
    }

    private AudioSource GetAvailableSource()
    {
        foreach (var source in sources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        if (sources.Count < maxSources)
        {
            return CreateNewSource();
        }

        var oldestSources = sources[0];
        sources.RemoveAt(0);
        sources.Add(oldestSources);
        return oldestSources;
    }
}
