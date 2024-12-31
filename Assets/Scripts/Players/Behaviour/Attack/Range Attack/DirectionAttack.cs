using UnityEngine;

public class DirectionAttack : DetectAttackClass
{
    [Header("생성할 공격용 프리팹")]
    [SerializeField] private GameObject rangePrefab;

    [Header("공격 딜레이")]
    [SerializeField] protected float attackDelay;
    protected float lastAttackTime;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    protected override void InitValue()
    {
        detectedTargetCount = 1;
    }

    protected override void Update()
    {
        base.Update();

        PerformAttack();
    }

    protected override void PerformAttack()
    {
        AttackToDirection();
    }

    private void AttackToDirection()
    {
        if (Time.time - lastAttackTime < attackDelay || detectedTargets.Count <= 0) return;

        Vector2 attackDirection = playerMovement.GetInputVector();

        if (attackDirection == Vector2.zero)
        {
            attackDirection = playerMovement.GetLastInputVector();
        }

        if (rangePrefab != null)
        {
            GameObject attackObject = Instantiate(rangePrefab, transform.position, Quaternion.identity);
            if (attackObject.TryGetComponent<IRangeObject>(out var attackScript))
            {
                attackScript.InitDirection(attackDirection);
            }

            lastAttackTime = Time.time;
        }
    }
}
