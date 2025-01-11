using UnityEngine;

public class GetterItem : MonoBehaviour
{
    [Header("아이템 흡수 설정")]
    [SerializeField] private float absorbRange = 1.3f; 
    [SerializeField] private float absorbSpeed = 5f;
    [SerializeField] private LayerMask itemLayer; 

    private void Update()
    {
        AbsorbItems();
    }

    private void AbsorbItems()
    {
        Collider2D[] itemsInRange = Physics2D.OverlapCircleAll(transform.position, absorbRange, itemLayer);

        foreach (Collider2D item in itemsInRange)
        {
            Vector2 direction = (transform.position - item.transform.position).normalized;
            item.transform.position += (Vector3)(direction * absorbSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, item.transform.position) < 0.1f)
            {
                CollectItem(item.gameObject);
            }
        }
    }

    private void CollectItem(GameObject item)
    {
        if (item.TryGetComponent<IItemBehaviour>(out var itemBehaviour))
        {
            itemBehaviour.OnBehaviour(gameObject);
        }

        ObjectPoolManager.Instance.ReturnToPool(item);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, absorbRange);
    }
}
