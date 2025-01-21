using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LateralAttack : DetectAttackClass
{
    private List<Collider2D> leftTargets = new List<Collider2D>();
    private List<Collider2D> rightTargets = new List<Collider2D>();

    [SerializeField] private float detectionWidth = 5f;
    [SerializeField] private float detectionHeight = 5f;

    protected override void InitValue()
    {
        base.InitValue();
        detectedTargetCount = 2;
    }

    protected override void DetectTargets()
    {
        leftTargets.Clear();
        rightTargets.Clear();

        if (health.GetIsDead() || StageManager.Instance.GetIsStageEnd())
        {
            detectedTargets.Clear();
            return;
        }

        RaycastForTargets(Vector2.left, leftTargets);
        RaycastForTargets(Vector2.right, rightTargets);

        detectedTargets.AddRange(leftTargets);
        detectedTargets.AddRange(rightTargets);

        detectedTargets = detectedTargets.OrderBy(t => Vector2.Distance(transform.position, t.transform.position)).ToList();
        detectedTargets = detectedTargets.Take(detectedTargetCount).ToList();

        PerformAttack();
    }

    private void RaycastForTargets(Vector2 direction, List<Collider2D> targetList)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + detectPos, direction, detectionWidth, targetLayer);

        foreach (var hit in hits)
        {
            if (Mathf.Abs(hit.point.y - transform.position.y) <= detectionHeight)
            {
                HealthParent targetHealth = hit.collider.GetComponent<HealthParent>();
                if (targetHealth != null && !targetHealth.GetIsDead())
                {
                    targetList.Add(hit.collider);
                }
            }
        }
    }

    protected override void PerformAttack()
    {
        AttackLateral();
    }

    private void AttackLateral()
    {
        if ((leftTargets.Count == 0 && rightTargets.Count == 0) || Time.time - lastAttackTime < attackDelay)
            return;

        int remainingTargets = detectedTargetCount;

        int leftProcessed = 0;
        foreach (var target in leftTargets)
        {
            if (remainingTargets <= 0) break;
            AttackTarget(target);
            leftProcessed++;
            remainingTargets--;
        }

        int rightProcessed = 0;
        foreach (var target in rightTargets)
        {
            if (remainingTargets <= 0) break; 
            AttackTarget(target);
            rightProcessed++;
            remainingTargets--;
        }

        lastAttackTime = Time.time; // 공격 시간 업데이트
    }

    private void AttackTarget(Collider2D target)
    {
        if (target == null) return;

        var attack = ObjectPoolManager.Instance.GetFromPool(attackPrefab);
        attack.transform.SetPositionAndRotation(target.transform.position, Quaternion.identity);

        if (target.TryGetComponent<IHit>(out var health))
        {
            health.TakeDamage(attackPoint);
        }
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + detectPos, transform.position + detectPos + Vector3.left * detectionWidth);
        Gizmos.DrawLine(transform.position + detectPos, transform.position + detectPos + Vector3.right * detectionWidth);
    }
}
