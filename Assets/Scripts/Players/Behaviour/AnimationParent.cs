using UnityEngine;

public abstract class AnimationParent : MonoBehaviour
{
    protected readonly string IdleName = $"Idle";
    protected readonly string DeadName = $"Dead";
    protected readonly string HitName = $"Hit";

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected MovementParent movement;
    protected ShadowOffset shadowOffset;

    protected virtual void Awake()
    {
        GetComponents();
    }

    protected virtual void OnEnable()
    {
        shadowOffset.ShadowObjectActive(true);
    }

    protected virtual void GetComponents()
    {
        movement = GetComponent<MovementParent>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shadowOffset = GetComponent<ShadowOffset>();
    }

    protected void LateUpdate()
    {
        ChangeAnimation();
    }

    protected abstract void ChangeAnimation();

    public virtual Animator IdleAnimator()
    {
        animator.SetTrigger(IdleName);
        return animator;
    }

    public virtual Animator DeadAnimator()
    {
        shadowOffset.ShadowObjectActive(false);
        animator.SetTrigger(DeadName);
        movement.GetDontMove();
        return animator;
    }

    public virtual Animator HitAnimator()
    {
        animator.SetTrigger(HitName);
        return animator;
    }

    public Animator GetAnimator() => animator;
}
