using System.Collections;
using UnityEngine;

public class EnemyHealth : HealthParent
{
    private Collider2D col;

    protected override void Awake()
    {
        base.Awake();
        GetCollider();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        InitCollider();
    }

    private void GetCollider()
    {
        col = GetComponent<Collider2D>();
    }

    protected void InitCollider()
    {
        col.enabled = true;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        animator.HitAnimator();
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

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
