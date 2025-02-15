using UnityEngine;
public class TitleButton : MonoBehaviour
{
    public void TitleButtonClick()
    {
        LoadingManager.LoadScene($"Title");
    }
}
