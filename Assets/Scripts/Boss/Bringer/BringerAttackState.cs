public class BringerAttackState : BossAttackState
{
    #region FSM State
    public override void EnterState(BossFSMController.E_State state)
    {
        animator.SetInteger(Animator_ParamName, (int)state);
    }

    public override void ExitState()
    {
        
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
            controller.TransitionToState(BossFSMController.E_State.Idle);
            return;
        }
    }
}
