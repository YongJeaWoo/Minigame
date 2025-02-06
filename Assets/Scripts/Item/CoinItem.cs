using UnityEngine;

public class CoinItem : MonoBehaviour, IItemBehaviour
{
    private int amount;

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
        PlayerManager.Instance.UpdateCoin(amount);
    }
}
