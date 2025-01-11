using UnityEngine;

public class HealthItem : MonoBehaviour, IItemBehaviour
{
    [SerializeField] private float healAmount;

    public void OnBehaviour(GameObject collector)
    {
        if (collector.TryGetComponent<PlayerHealth>(out var player))
        {
            player.RestoreHealth(healAmount);
        }
    }
}
