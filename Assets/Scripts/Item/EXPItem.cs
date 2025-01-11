using UnityEngine;

public class EXPItem : MonoBehaviour, IItemBehaviour
{
    [SerializeField] private float expAmount;

    public void OnBehaviour(GameObject collector)
    {
        if (collector.TryGetComponent<PlayerExp>(out var player))
        {
            player.AddExp(expAmount);
        }
    }
}
