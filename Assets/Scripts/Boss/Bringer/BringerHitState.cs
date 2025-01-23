using UnityEngine;

public class BringerHitState : BossAttackState
{
    protected bool isHit;
    public bool IsHit { get => isHit; set => isHit = value; }

    private BossHealthParent health;

    protected override void Awake()
    {
        base.Awake();
        GetHealthComponent();
    }

    private void GetHealthComponent()
    {
        health = GetComponent<BossHealthParent>();
    }

    #region FSM State
    public override void EnterState(BossFSMController.E_State state)
    {
        InitHit(state);
    }

    public override void ExitState()
    {
        ExitValue();
    }

    public override void UpdateState()
    {
        HitBehaviour();
    }
    #endregion

    #region InitValue
    private void InitHit(BossFSMController.E_State state)
    {
        IsHit = true;
        animator.SetInteger(Animator_ParamName, (int)state);
    }
    #endregion

    #region ExitValue
    private void ExitValue()
    {
        IsHit = false;
    }
    #endregion

    private void HitBehaviour()
    {
        if (IsHit || health.GetHealth() < 0) return;

        if (controller.GetPlayerDistance() <= attackDistance)
        {
            var randomValue = Random.Range(0f, 1f);

            if (randomValue <= 0.7f)
            {
                controller.TransitionToState(BossFSMController.E_State.Attack);
            }
            else
            {
                controller.TransitionToState(BossFSMController.E_State.SpecialAttack);
            }
            
            return;
        }
        else
        {
            controller.TransitionToState(BossFSMController.E_State.Idle);
            return;
        }
    }
}
