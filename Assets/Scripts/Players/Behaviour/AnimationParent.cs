using UnityEngine;

public abstract class AnimationParent : MonoBehaviour
{
    protected readonly string IdleName = $"Idle";
    protected readonly string DeadName = $"Dead";
    protected readonly string HitName = $"Hit";

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected MovementParent movement;

    private GameObject shadowObject;

    protected virtual void Awake()
    {
        GetComponents();
    }

    protected virtual void OnEnable()
    {
        ShadowObjectActive(true);
    }

    protected virtual void GetComponents()
    {
        movement = GetComponent<MovementParent>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        shadowObject = transform.GetChild(0).gameObject;
    }

    protected void LateUpdate()
    {
        ChangeAnimation();
    }

    protected abstract void ChangeAnimation();

    protected void UpdateShadowPosition(Vector2 value)
    {
        float offsetX = value.x == 0 ?
            (spriteRenderer.flipX ? -movement.GetShadowOffset().x : movement.GetShadowOffset().x) :
            (value.x < 0 ? -movement.GetShadowOffset().x : movement.GetShadowOffset().x);

        float offsetY = movement.GetShadowOffset().y;

        Vector3 playerPos = transform.position;
        Vector3 shadowPosition = new(playerPos.x + offsetX, playerPos.y + offsetY, playerPos.z);

        shadowObject.transform.position = shadowPosition;
    }

    public virtual Animator IdleAnimator()
    {
        animator.SetTrigger(IdleName);
        return animator;
    }

    public virtual Animator DeadAnimator()
    {
        ShadowObjectActive(false);
        animator.SetTrigger(DeadName);
        movement.GetDontMove();
        return animator;
    }

    public virtual Animator HitAnimator()
    {
        animator.SetTrigger(HitName);
        return animator;
    }

    protected void ShadowObjectActive(bool isOn) => shadowObject.SetActive(isOn);

    public Animator GetAnimator() => animator;
}
