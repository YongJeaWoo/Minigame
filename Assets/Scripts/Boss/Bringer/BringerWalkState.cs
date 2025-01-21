using UnityEngine;

public class BringerWalkState : BossAttackState
{
    private Vector2 targetPosition;
    private float moveDuration;
    private float elapsedTime;

    [Header("이동 속도와 지속 시간")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxMoveTime;

    #region FSM State
    public override void EnterState(BossFSMController.E_State state)
    {
        InitWalk(state);
    }

    public override void ExitState()
    {
        ExitWalk();
    }

    public override void UpdateState()
    {
        WalkBehaviour();
    }
    #endregion

    #region Enter Value
    private void InitWalk(BossFSMController.E_State state)
    {
        targetPosition = controller.Player.transform.position;

        moveDuration = maxMoveTime;
        elapsedTime = 0;

        animator.SetInteger(Animator_ParamName, (int)state);
    }
    #endregion

    #region Exit Value
    private void ExitWalk()
    {
        elapsedTime = 0;
    }
    #endregion

    private void WalkBehaviour()
    {
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            controller.TransitionToState(BossFSMController.E_State.Attack);
            return;
        }

        elapsedTime += Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (elapsedTime >= moveDuration || Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            controller.TransitionToState(BossFSMController.E_State.Idle);
            return;
        }
    }
}
