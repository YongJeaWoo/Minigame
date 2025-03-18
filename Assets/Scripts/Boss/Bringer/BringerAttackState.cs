using UnityEngine;

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
        
    }
    #endregion

    public void AttackBehaviour()
    {
        if (controller.GetPlayerDistance() > attackDistance)
        {
            int randomValue = Random.Range(0, 2);

            switch (randomValue)
            {
                case 0:
                    {
                        controller.TransitionToState(BossFSMController.E_State.Idle);
                    }
                    break;
                case 1:
                    {
                        controller.TransitionToState(BossFSMController.E_State.SpecialAttack);
                    }
                    break;
            }
        }
    }
}
