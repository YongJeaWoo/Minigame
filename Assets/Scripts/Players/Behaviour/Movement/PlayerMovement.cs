using UnityEngine;

public class PlayerMovement : MovementParent
{
    private Vector2 lastInputVec;

    private void Start()
    {
        InitValue();
    }

    private void InitValue()
    {
        lastInputVec = Vector2.right;
    }

    private void Update()
    {
        InputKeys();
    }

    private void InputKeys()
    {
        if (DontMove()) return;

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (direction != Vector2.zero)
        {
            lastInputVec = direction;
        }
    }

    protected override void ObjectMovement()
    {
        Vector2 nextVec = moveSpeed * Time.fixedDeltaTime * direction.normalized;
        rigid.MovePosition(rigid.position + nextVec);
    }
    
    public Vector2 GetLastInputVector() => lastInputVec.normalized;
}
