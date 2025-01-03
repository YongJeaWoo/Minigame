using System.Collections;
using UnityEngine;

public class EnemyAnimation : AnimationParent
{
    private Transform player;

    protected override void OnEnable()
    {
        base.OnEnable();
        FindPlayer();
    }

    private void FindPlayer()
    {
        player = PlayerManager.Instance.GetPlayer().transform;
    }

    protected override void ChangeAnimation()
    {
        if (player != null)
        {
            spriteRenderer.flipX = player.position.x < transform.position.x;
        }
    }

    public Transform SetNullPlayer() => player = null;
    public Transform GetPlayer() => player;

    public override Animator IdleAnimator()
    {
        player = null;
        return base.IdleAnimator();
    }

    public override Animator DeadAnimator()
    {
        player = null;
        return base.DeadAnimator();
    }

    public override Animator HitAnimator()
    {
        animator.SetTrigger(HitName);
        StartCoroutine(ResetHitState());
        return animator;
    }

    private IEnumerator ResetHitState()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        while (stateInfo.IsName(HitName) && stateInfo.normalizedTime < 1f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }
    }
}
