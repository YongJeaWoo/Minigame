using UnityEngine;

public class BringerIdleState : BossAttackState
{
    protected float time;
    protected float checkTime;
    [Header("랜덤 대기 시간")]
    [SerializeField] protected Vector2 randomCheckTimer;

    #region FSM State
    public override void EnterState(BossFSMController.E_State state)
    {
        InitIdle(state);
    }

    public override void ExitState()
    {
        ExitValue();
    }

    public override void UpdateState()
    {
        IdleBehaviour();
    }
    #endregion

    #region Enter Value
    private void InitIdle(BossFSMController.E_State state)
    {
        checkTime = Random.Range(randomCheckTimer.x, randomCheckTimer.y);
        time = 0;

        animator.SetInteger(Animator_ParamName, (int)state);
    }
    #endregion

    #region Exit Value
    private void ExitValue()
    {
        time = 0;
    }
    #endregion

    #region Behaviour
    private void IdleBehaviour()
    {
        time += Time.deltaTime;
        AttackPlayer();

    }

    private void AttackPlayer()
    {
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            controller.TransitionToState(BossFSMController.E_State.Attack);
            return;
        }

        RandomBehaviour();
    }

    private void RandomBehaviour()
    {
        if (time > checkTime)
        {
            float[] weights = { 1f, 6f, 3f };
            float totalWeight = 0;

            foreach (float weight in weights)
            {
                totalWeight += weight;
            }

            float randomValue = Random.Range(0, totalWeight);
            float cumulativeWeight = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                cumulativeWeight += weights[i];
                if (randomValue <= cumulativeWeight)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                time = 0;
                                checkTime = Random.Range(randomCheckTimer.x, randomCheckTimer.y);
                            }
                            break;
                        case 1:
                            {
                                controller.TransitionToState(BossFSMController.E_State.SpecialAttack);
                                return;
                            }
                        case 2:
                            {
                                controller.TransitionToState(BossFSMController.E_State.Walk);
                                return;
                            }
                    }
                    break;
                }
            }
        }
    }
    #endregion
}
