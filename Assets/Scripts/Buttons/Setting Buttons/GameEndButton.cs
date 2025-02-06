using UnityEngine;

public class GameEndButton : MonoBehaviour
{
    public void GameEndButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
