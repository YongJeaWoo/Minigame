using UnityEngine;

public class EXPItem : MonoBehaviour, IItemBehaviour
{
    [SerializeField] private float expAmount;
    [SerializeField] private AudioClip getItemClip;

    public void OnBehaviour(GameObject collector)
    {
        if (collector.TryGetComponent<PlayerExp>(out var player))
        {
            AudioManager.Instance.PlaySFX(getItemClip);
            player.AddExp(expAmount);
        }
    }
}
