using UnityEngine;

public class EffectSound : MonoBehaviour
{
    [SerializeField] private AudioClip effectSound;

    private void OnEnable()
    {
        SoundPlay();
    }

    private void SoundPlay()
    {
        AudioManager.Instance.PlaySFX(effectSound);
    }
}
