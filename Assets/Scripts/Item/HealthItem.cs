using UnityEngine;

public class HealthItem : MonoBehaviour, IItemBehaviour
{
    [SerializeField] private float healAmount;
    [SerializeField] private AudioClip getItemClip;

    public void OnBehaviour(GameObject collector)
    {
        if (collector.TryGetComponent<PlayerHealth>(out var player))
        {
            AudioManager.Instance.PlaySFX(getItemClip);
            player.RestoreHealth(healAmount);
        }
    }
}
