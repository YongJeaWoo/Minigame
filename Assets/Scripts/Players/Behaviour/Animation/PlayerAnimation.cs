using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private readonly string animParaName = $"Speed";

    private PlayerMovement pMovement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        pMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        ChangeAnimationToInput();
    }

    private void ChangeAnimationToInput()
    {
        var inputVec = pMovement.GetInputVector();

        animator.SetFloat(animParaName, inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0;
        }

        UpdateShadowPosition(inputVec);
    }

    private void UpdateShadowPosition(Vector2 value)
    {
        float offsetX = value.x == 0 ? 
            (spriteRenderer.flipX ? -pMovement.GetShadowOffset().x : pMovement.GetShadowOffset().x) :
            (value.x < 0 ? -pMovement.GetShadowOffset().x : pMovement.GetShadowOffset().x);

        float offsetY = pMovement.GetShadowOffset().y;

        Vector3 playerPos = transform.position;
        Vector3 shadowPosition = new(playerPos.x + offsetX, playerPos.y + offsetY, playerPos.z);

        transform.GetChild(0).position = shadowPosition;
    }
}
