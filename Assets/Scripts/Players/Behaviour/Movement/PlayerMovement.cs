using UnityEngine;

public class PlayerMovement : MovementParent
{
    private Vector2 lastInputVec;

    private PlayerData playerData;
    private CoinUpgradeValue upgradeValue;

    private void Start()
    {
        InitValue();
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        playerData = GetComponent<PlayerInfoData>().GetPlayerData();
        upgradeValue = GetComponent<CoinUpgradeValue>();
    }

    private void InitValue()
    {
        BaseStatePlayer();
    }

    private void BaseStatePlayer()
    {
        lastInputVec = Vector2.right;
        float baseMovement = playerData.GetMoveSpeed();
        float upgradeMovement = (upgradeValue != null) ? upgradeValue.GetMovementUpgrade() : 0f;

        moveSpeed = baseMovement + upgradeMovement;
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
