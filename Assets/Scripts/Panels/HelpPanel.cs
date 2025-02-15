using UnityEngine;

public class HelpPanel : InteractionPanel
{
    [SerializeField] private GameObject[] explains;

    private int index = 0;
    private readonly string IsNextName = $"isNext";
    private readonly string IsPrevName = $"isPrevious";

    public void NextButton()
    {
        if (index < explains.Length - 1)
        {
            index++;
            animator.SetTrigger(IsNextName);
        }
    }

    public void PrevButton()
    {
        if (index > 0)
        {
            index--;
            animator.SetTrigger(IsPrevName);
        }
    }
}
