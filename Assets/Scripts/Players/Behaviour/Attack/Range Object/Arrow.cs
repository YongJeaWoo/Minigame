using UnityEngine;

public class Arrow : MonoBehaviour, IRangeObject
{
    private Vector2 direction;

    [Header("적 레이어")]
    [SerializeField] private LayerMask targetLayer;
    [Header("움직임 속도")]
    [SerializeField] private float moveSpeed;
    [Header("공격력")]
    [SerializeField] private float damaged;

    public void InitDirection(Vector2 attackDirection)
    {
        direction = attackDirection;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (direction != Vector2.zero)
        {
            transform.Translate(moveSpeed * Time.deltaTime * direction, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TargetContact(collision);
    }

    private void TargetContact(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            if (collision.TryGetComponent<HealthParent>(out var health))
            {
                health.TakeDamage(damaged);
            }

            Destroy(gameObject);
        }
    }
}
