public class PlayerAnimation : AnimationParent
{
    private readonly string animParamName = $"Speed";

    protected override void ChangeAnimation()
    {
        var inputVec = movement.GetInputVector();

        animator.SetFloat(animParamName, inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0;
        }

        shadowOffset.UpdateShadowPosition(inputVec);
    }
}
