using UnityEngine;

public class EnemyMovement : MovementParent
{
    private EnemyAnimation enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<EnemyAnimation>();
    }

    protected override void ObjectMovement()
    {
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
}
