using System.Collections.Generic;
using UnityEngine;

public class OmniAttack : DetectAttackClass
{
    private List<Collider2D> sortedTargets = new List<Collider2D>();

    protected override void InitValue()
    {
        detectedTargetCount = 1;
    }

    protected override void Update()
    {
        base.Update();
        DetectOmniTargets();
    }

    private void DetectOmniTargets()
    {
        if (detectedTargetCount > 0)
        {
            PerformAttack();
        }
    }

    protected override void PerformAttack()
    {
        if (sortedTargets.Count == 0) return;

        Debug.Log($"전방향 공격 중");
    }
}
