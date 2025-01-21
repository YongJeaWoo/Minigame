using System.Collections.Generic;
using UnityEngine;

public abstract class DetectAttackClass : MonoBehaviour
{
    [Header("Ÿ�� ���̾�")]
    [SerializeField] protected LayerMask targetLayer;
    [Header("���� ����")]
    [SerializeField] protected float attackRange = 1.5f;
    [Header("Ž�� ��ġ ��")]
    [SerializeField] protected Vector3 detectPos;
    [Header("������ ���ݿ� ������")]
    [SerializeField] protected GameObject attackPrefab;
    [Header("���� ������")]
    [SerializeField] protected float attackDelay;
    protected float lastAttackTime;

    protected int detectedTargetCount;

    protected float attackPoint;

    protected List<Collider2D> detectedTargets = new List<Collider2D>();

    protected PlayerHealth health;
    protected PlayerData playerData;

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
        playerData = GetComponent<PlayerInfoData>().GetPlayerData();
        attackPoint = playerData.GetAttackPoint();
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
            IHit targetHealth = sortedTargets[i].GetComponent<IHit>();
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

    public void IncreaseDetectedTargets(UpgradeData data, int count, int maxCount)
    {
        if (data != null && count < maxCount)
        {
            detectedTargetCount += count;
        }
    }

    protected abstract void PerformAttack();
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + detectPos, attackRange);
    }

    public float SetAttackPoint(float value) => attackPoint += (attackPoint * value);
    public float GetAttackPoint() => attackPoint;

    public float SetAttackDelay(float value) => attackDelay -= value;
    public float SetAttackRange(float value) => attackRange += value;
}
