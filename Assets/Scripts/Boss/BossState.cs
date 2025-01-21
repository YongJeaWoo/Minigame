using UnityEngine;

public abstract class BossState : MonoBehaviour
{
    protected BossFSMController controller;
    protected Animator animator;

    protected string Animator_ParamName = $"State";
    
    protected virtual void Awake()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        controller = GetComponent<BossFSMController>();
        animator = GetComponent<Animator>();
    }

    public abstract void EnterState(BossFSMController.E_State state);
    public abstract void UpdateState();
    public abstract void ExitState();
}
