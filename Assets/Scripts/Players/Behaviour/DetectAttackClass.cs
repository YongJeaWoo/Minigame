using System.Collections.Generic;
using UnityEngine;

public abstract class DetectAttackClass : MonoBehaviour
{
    [Header("타겟 레이어")]
    [SerializeField] protected LayerMask targetLayer;
    [Header("탐지 위치 값")]
    [SerializeField] protected Vector3 detectPos;
    [Header("공격 사운드")]
    [SerializeField] protected AudioClip attackSoundClip;
    [Header("생성할 공격용 프리팹")]
    [SerializeField] protected GameObject attackPrefab;
    protected float lastAttackTime;

    protected int detectedTargetCount;

    protected float attackPoint;
    protected float attackRange;
    protected float attackDelay;

    protected List<Collider2D> detectedTargets = new List<Collider2D>();

    protected PlayerHealth health;
    protected PlayerData playerData;
    protected CoinUpgradeValue upgradeValue;

    protected virtual void Start()
    {
        InitValue();
    }

    protected virtual void Update()
    {
        Debug.Log($"Attack {attackPoint}");
        DetectTargets();
    }

    protected virtual void InitValue()
    {
        health = GetComponent<PlayerHealth>();
        playerData = GetComponent<PlayerInfoData>().GetPlayerData();
        upgradeValue = GetComponent<CoinUpgradeValue>();

        BaseStatePlayer();
    }

    private void BaseStatePlayer()
    {
        float baseAttackPoint = playerData.GetAttackPoint();
        float baseAttackDelay = playerData.GetAttackDelay();
        float baseAttackRange = playerData.GetAttackRange();

        float upgradeAttackPoint = (upgradeValue != null) ? upgradeValue.GetAttackPointUpgrade() : 0f;
        float upgradeAttackDelay = (upgradeValue != null) ? upgradeValue.GetAttackDelayUpgrade() : 0f;
        float upgradeAttackRange = (upgradeValue != null) ? upgradeValue.GetAttackRangeUpgrade() : 0f;

        attackPoint = baseAttackPoint + upgradeAttackPoint;
        attackDelay = baseAttackDelay - upgradeAttackDelay;
        attackRange = baseAttackRange + upgradeAttackRange;

        Debug.Log($"플레이어의 초기 스탯 - 공격력: {attackPoint}, 공격 딜레이: {attackDelay}, 공격 범위: {attackRange}");
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
