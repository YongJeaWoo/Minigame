using System.Collections;
using UnityEngine;

public class BringerAttackState : BossAttackState
{
    private bool isMissing;

    private Coroutine missing_coroutine;

    #region FSM State
    public override void EnterState(BossFSMController.E_State state)
    {
        isMissing = false;
        animator.SetInteger(Animator_ParamName, (int)state);
    }

    public override void ExitState()
    {
        isMissing = true;
    }

    public override void UpdateState()
    {
        AttackBehaviour();
    }
    #endregion

    private void AttackBehaviour()
    {
        if (controller.GetPlayerDistance() > attackDistance)
        {
            missing_coroutine = StartCoroutine(MissingPlayer());
            return;
        }
    }

    private IEnumerator MissingPlayer()
    {
        var attackState = animator.GetCurrentAnimatorStateInfo(0);

        while (attackState.normalizedTime < 1.0f)
        {
            attackState = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        controller.TransitionToState(BossFSMController.E_State.Idle);
        yield break;
    }
}
