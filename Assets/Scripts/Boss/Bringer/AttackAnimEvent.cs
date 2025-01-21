using UnityEngine;

public class AttackAnimEvent : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Collider2D attackArea;
    [SerializeField] private float attackDamage;

    public void AttackPlayer()
    {
        Collider2D[] hitTargets = Physics2D.OverlapBoxAll(attackArea.bounds.center, attackArea.bounds.size, 0, targetLayer);

        foreach (var target in hitTargets)
        {
            if (target.TryGetComponent<PlayerHealth>(out var health))
            {
                health.TakeDamage(attackDamage);
            }
        }
    }
}
