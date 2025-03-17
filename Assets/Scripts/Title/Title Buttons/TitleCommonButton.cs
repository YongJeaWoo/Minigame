using UnityEngine;

public abstract class TitleCommonButton : MonoBehaviour
{
    [SerializeField] protected AudioClip buttonClickSound;

    public abstract void ButtonClickedBehaviour();
}
