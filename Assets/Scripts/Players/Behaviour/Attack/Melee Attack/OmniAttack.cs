using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OmniAttack : DetectAttackClass
{
    private List<Collider2D> sortedTargets = new List<Collider2D>();

    protected override void InitValue()
    {
        base.InitValue();
        detectedTargetCount = 1;
    }

    protected override void Update()
    {
        base.Update();
        DetectOmniTargets();
    }

    private void DetectOmniTargets()
    {
        sortedTargets = detectedTargets
            .OrderBy(target => Vector2.Distance(transform.position, target.transform.position))
            .Take(detectedTargetCount)
            .ToList();

        if (sortedTargets.Count > 0)
        {
            PerformAttack();
        }
    }

    protected override void PerformAttack()
    {
        AttackOmni();
    }

    private void AttackOmni()
    {
        if (sortedTargets.Count == 0 || Time.time - lastAttackTime < attackDelay) return;

        foreach (var target in sortedTargets)
        {
            if (target != null)
            {
                var attack = ObjectPoolManager.Instance.GetFromPool(attackPrefab);
                attack.transform.SetPositionAndRotation(target.transform.position, Quaternion.identity);

                if (target.TryGetComponent<EnemyHealth>(out var health))
                {
                    health.TakeDamage(attackPoint);
                }
            }
        }

        lastAttackTime = Time.time;
    }
}
