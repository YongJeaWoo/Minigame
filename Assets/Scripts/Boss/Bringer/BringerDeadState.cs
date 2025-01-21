using UnityEngine;

public class BringerDeadState : BossState
{
    protected float time;
    [SerializeField] protected float deathDelayTime;

    #region FSM State
    public override void EnterState(BossFSMController.E_State state)
    {
        InitDeath(state);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        DeathBehaviour();
    }
    #endregion

    #region InitValue
    private void InitDeath(BossFSMController.E_State state)
    {
        animator.SetInteger(Animator_ParamName, (int)state);
    }
    #endregion

    private void DeathBehaviour()
    {
        time += Time.deltaTime;

        if (time >= deathDelayTime)
        {
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }
}
