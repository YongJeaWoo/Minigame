using System.Collections;
using UnityEngine;

public class EnemyMovement : MovementParent
{
    private EnemyAnimation enemy;
    private bool isStopped = false;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<EnemyAnimation>();
    }

    protected override void ObjectMovement()
    {
        if (isStopped) return;

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
        if (!isStopped) 
        {
            isStopped = true; 
            StartCoroutine(ResetMovement(duration)); 
        }
    }

    private IEnumerator ResetMovement(float duration)
    {
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }
}
