using UnityEngine;

public class NearDetectedAttack : DirectionAttack
{
    protected override void PerformAttack()
    {
        NearDetected();
    }

    private void NearDetected()
    {
        if (Time.time - lastAttackTime < attackDelay || detectedTargets.Count <= 0) return;

        Collider2D closestTarget = detectedTargets[0];
        float closestDistance = Vector2.Distance(transform.position, closestTarget.transform.position);

        foreach (var target in detectedTargets)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        if (rangePrefab != null && closestTarget != null)
        {
            Vector3 spawnPosition = transform.position;

            GameObject attackObject = ObjectPoolManager.Instance.GetFromPool(rangePrefab);
            attackObject.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);

            if (attackObject.TryGetComponent<IRangeObject>(out var attackScript))
            {
                Vector2 attackDirection = (closestTarget.transform.position - spawnPosition).normalized;
                attackScript.InitDirection(attackDirection, this);
            }

            lastAttackTime = Time.time;
        }
    }
}
