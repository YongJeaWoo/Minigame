using UnityEngine;

public class GameEndButton : MonoBehaviour
{
    private readonly string GameEndPanel = $"Game End Panel";

    public void GameEndButtonClick()
    {
        PopupManager.Instance.AddPopup(GameEndPanel);
    }
}
