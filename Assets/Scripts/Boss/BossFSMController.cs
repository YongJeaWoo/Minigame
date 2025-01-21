using UnityEngine;

public class BossFSMController : MonoBehaviour
{
    public enum E_State
    {
        Idle,
        Walk,
        Attack,
        SpecialAttack,
        Hit,
        Dead
    }

    private GameObject player;
    public GameObject Player { get; private set; }

    private BossState currentState;
    [SerializeField] private BossState[] bossStates;

    private void Start()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        Player = PlayerManager.Instance.GetPlayer();
        TransitionToState(E_State.Idle);
    }

    private void Update()
    {
        LookAtTarget();
        currentState?.UpdateState();
    }

    public void TransitionToState(E_State state)
    {
        currentState?.ExitState();
        currentState = bossStates[(int)state];
        currentState.EnterState(state);
    }

    public void TakeHit()
    {
        if (currentState == bossStates[(int)E_State.Dead]) return;

        TransitionToState(E_State.Hit);
    }

    public float GetPlayerDistance()
    {
        return Vector2.Distance(transform.position, Player.transform.position);
    }

    protected void LookAtTarget()
    {
        if (Player != null)
        {
            Vector3 scale = transform.localScale;
            scale.x = Player.transform.position.x < transform.position.x ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
