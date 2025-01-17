using UnityEngine;

public class PlayerMovement : MovementParent
{
    private Vector2 lastInputVec;

    private PlayerData playerData;

    private void Start()
    {
        InitValue();
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        playerData = GetComponent<PlayerInfoData>().GetPlayerData();
    }

    private void InitValue()
    {
        lastInputVec = Vector2.right;
        moveSpeed = playerData.GetMoveSpeed();
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

    public float SetMoveSpeed(float value) => moveSpeed += (moveSpeed * value);
}
