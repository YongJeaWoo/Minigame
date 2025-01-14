using System.Collections;
using UnityEngine;

public class EnemyHealth : HealthParent
{
    private Collider2D col;
    private ItemDrop itemDrop;
    private EnemyMovement movement;

    [SerializeField] private float pauseTimer;

    protected override void Awake()
    {
        base.Awake();
        InitGetComponents();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        InitCollider();
    }

    private void InitGetComponents()
    {
        col = GetComponent<Collider2D>();
        itemDrop = GetComponent<ItemDrop>();
        movement = GetComponent<EnemyMovement>();
    }

    protected void InitCollider()
    {
        col.enabled = true;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        animator.HitAnimator();

        if (movement != null)
        {
            movement.StopMovement(pauseTimer);
        }
    }
    
    protected override void Death()
    {
        col.enabled = false;
        base.Death();
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        var f_animator = animator.GetAnimator();
        AnimatorStateInfo stateInfo = f_animator.GetCurrentAnimatorStateInfo(0);

        while (!stateInfo.IsName("Dead"))
        {
            yield return null;
            stateInfo = f_animator.GetCurrentAnimatorStateInfo(0);
        }

        yield return new WaitForSeconds(stateInfo.length);

        itemDrop.Drop();

        yield return new WaitForSeconds(2f);

        ObjectPoolManager.Instance.ReturnToPool(gameObject);
    }
}
