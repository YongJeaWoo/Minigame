using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [Header("아이템 관련 정보")]
    [SerializeField] private GameObject[] items;
    [SerializeField] private float[] dropRates;
    [SerializeField] private float dropRange;

    public void Drop()
    {
        if (items.Length != dropRates.Length) return;
        
        for (int i = 0; i < items.Length; i++)
        {
            float randomValue = Random.value;
            if (randomValue <= dropRates[i])
            {
                Vector2 randomPos = new(Random.Range(-dropRange, dropRange),
                    Random.Range(-dropRange, dropRange));

                Vector2 spawnPos = (Vector2)transform.position + randomPos;

                var obj = ObjectPoolManager.Instance.GetFromPool(items[i]);
                if (obj != null)
                {
                    obj.transform.position = spawnPos;
                }
            }
        }
    }
}
