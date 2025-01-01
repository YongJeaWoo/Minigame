using System.Collections.Generic;
using UnityEngine;

public class LateralAttack : DetectAttackClass
{
    private List<Collider2D> leftTargets = new List<Collider2D>();
    private List<Collider2D> rightTargets = new List<Collider2D>();

    protected override void InitValue()
    {
        base.InitValue();
        detectedTargetCount = 2;
    }

    protected override void Update()
    {
        base.Update();
        DetectLateralTargets();
    }

    private void DetectLateralTargets()
    {
        leftTargets.Clear();
        rightTargets.Clear();

        foreach (var target in detectedTargets)
        {
            float direction = target.transform.position.x - transform.position.x;

            if (direction < 0 && leftTargets.Count < detectedTargetCount / 2)  // ¿ÞÂÊ¿¡ ÀÖ´Â Å¸°Ù
            {
                leftTargets.Add(target);
            }
            else if (direction > 0 && rightTargets.Count < detectedTargetCount / 2)
            {
                 rightTargets.Add(target);
            }
        }

        if (leftTargets.Count > 0 || rightTargets.Count > 0)
        {
            PerformAttack();
        }
    }

    protected override void PerformAttack()
    {
        if (leftTargets.Count == 0 && rightTargets.Count == 0) return;

        Debug.Log($"ÁÂ¿ì °ø°Ý Áß");
    }
}
