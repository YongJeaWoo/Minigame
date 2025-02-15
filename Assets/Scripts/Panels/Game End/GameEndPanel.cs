using UnityEngine;

public class GameEndPanel : MonoBehaviour
{
    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void GameEndCancel()
    {
        var parentName = gameObject.transform.parent.name;
        PopupManager.Instance.RemovePopup(parentName);
    }
}
