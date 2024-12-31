using UnityEngine;

public class Arrow : MonoBehaviour, IRangeObject
{
    private Vector2 direction;

    [Header("움직임 속도")]
    [SerializeField] private float moveSpeed;

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
}
