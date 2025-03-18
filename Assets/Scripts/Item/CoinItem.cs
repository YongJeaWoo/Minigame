using UnityEngine;

public class CoinItem : MonoBehaviour, IItemBehaviour
{
    private int amount;
    [SerializeField] private AudioClip getItemClip;

    private void OnEnable()
    {
        SetRandomAmount();
    }

    private void SetRandomAmount()
    {
        amount = Random.Range(1, 5);
    }

    public void OnBehaviour(GameObject collector)
    {
        AudioManager.Instance.PlaySFX(getItemClip);
        PlayerManager.Instance.UpdateCoin(amount);
    }
}
