using UnityEngine;

public class DirectionAttack : DetectAttackClass
{
    protected PlayerMovement playerMovement;

    protected virtual void Awake()
    {
        GetComponents();
    }

    protected void GetComponents()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    protected override void InitValue()
    {
        base.InitValue();
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

        if (attackPrefab != null)
        {
            Vector3 spawnPosition = transform.position;

            GameObject attackObject = ObjectPoolManager.Instance.GetFromPool(attackPrefab);
            attackObject.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);

            if (attackObject.TryGetComponent<IRangeObject>(out var attackScript))
            {
                attackScript.InitDirection(attackDirection, this);
            }

            lastAttackTime = Time.time;
        }
    }
}
