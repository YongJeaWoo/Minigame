using System.Collections.Generic;
using UnityEngine;

public abstract class DetectAttackClass : MonoBehaviour
{
    [Header("타겟 레이어")]
    [SerializeField] protected LayerMask targetLayer;
    [Header("공격 범위")]
    [SerializeField] protected float attackRange = 1.5f;
    [Header("탐지 위치 값")]
    [SerializeField] protected Vector3 detectPos;
    protected int maxDetectableTargets = 6;
    protected int detectedTargetCount;

    protected List<Collider2D> detectedTargets = new List<Collider2D>();

    private PlayerHealth health;

    protected virtual void Start()
    {
        InitValue();
    }

    protected virtual void Update()
    {
        DetectTargets();
    }

    protected virtual void InitValue()
    {
        health = GetComponent<PlayerHealth>();
    }

    protected virtual void DetectTargets()
    {
        if (health.GetIsDead() || StageManager.Instance.GetIsStageEnd())
        {
            detectedTargets.Clear();
            return;
        }

        Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(transform.position + detectPos, attackRange, targetLayer);

        List<Collider2D> sortedTargets = new List<Collider2D>(targetsInRange);
        sortedTargets.Sort((a, b) =>
            Vector2.Distance(transform.position, a.transform.position)
            .CompareTo(Vector2.Distance(transform.position, b.transform.position)));

        detectedTargets.Clear();

        for (int i = 0; i < Mathf.Min(detectedTargetCount, sortedTargets.Count); i++)
        {
            HealthParent targetHealth = sortedTargets[i].GetComponent<HealthParent>();
            if (targetHealth != null && !targetHealth.GetIsDead())
            {
                detectedTargets.Add(sortedTargets[i]);

                if (detectedTargets.Count >= detectedTargetCount)
                {
                    break;
                }
            }            
        }
    }

    public void IncreaseMaxTargets()
    {
        if (detectedTargetCount < maxDetectableTargets)
        {
            detectedTargetCount++;
        }
    }

    protected abstract void PerformAttack();
    
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + detectPos, attackRange);
    }
}
