using System.Collections;
using UnityEngine;

public class EnemyMovement : MovementParent
{
    private EnemyAnimation enemy;
    private bool isStopped;
    private bool isKnockBack;
    [SerializeField] private float knockBackForce;
    
    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<EnemyAnimation>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        isKnockBack = false;
        isStopped = false;
    }

    protected override void ObjectMovement()
    {
        if (isStopped || enemy.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        if (DontMove() || 
            enemy.GetPlayer() == null)
        {
            enemy.IdleAnimator();
            return;
        }

        Vector2 targetPos = enemy.GetPlayer().position;
        direction = (targetPos - (Vector2)transform.position).normalized;

        Vector2 nextVec = moveSpeed * Time.fixedDeltaTime * direction;
        rigid.MovePosition(rigid.position + nextVec);
    }

    public void StopMovement(float duration)
    {
        if (!isStopped && !health.GetIsDead())
        {
            isStopped = true; 
            StartCoroutine(KnockBack(duration)); 
        }
    }

    private IEnumerator KnockBack(float duration)
    {
        if (isKnockBack) yield break;

        isKnockBack = true;

        Vector2 targetPos = enemy.GetPlayer().position;
        direction = ((Vector2)transform.position - targetPos).normalized;
        rigid.velocity = direction * knockBackForce;

        yield return new WaitForSeconds(duration);

        isStopped = false;
        isKnockBack = false;
        rigid.velocity = Vector2.zero;
    }
}
