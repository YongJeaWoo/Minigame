using UnityEngine;
using UnityEngine.UI;

public abstract class BehaviourButton : MonoBehaviour
{
    protected Button myButton;
    protected Image iconImage;

    [SerializeField] protected Sprite xIcon;
    [SerializeField] protected Sprite behaviourIcon;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        iconImage = GetComponent<Image>();
    }

    protected virtual void Start()
    {
        SetButton();
    }

    private void SetButton()
    {
        myButton.onClick.AddListener(SetBehaviour);
    }

    public abstract void SetBehaviour();
}
