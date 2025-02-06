using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionPanel : MonoBehaviour
{
    protected readonly string isOpenText = $"isOpen";

    [SerializeField] protected Button exitButton;

    [Header("정보 텍스트")]
    [SerializeField] private TextMeshProUGUI infoText;

    protected Animator animator;

    protected virtual void Awake()
    {
        InitValues();
    }

    protected virtual void InitValues()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(isOpenText, true);

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitButton);
        }
    }

    public void ExitAnyClick(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        if (pointerEventData == null) return;

        RectTransform panelRect = transform.GetComponent<RectTransform>();

        bool outSidePanel = !RectTransformUtility.RectangleContainsScreenPoint(panelRect, pointerEventData.position);

        if (outSidePanel)
        {
            StartCoroutine(RemovePopupCoroutine());
        }
    }

    public void ExitButton()
    {
        StartCoroutine(RemovePopupCoroutine());
    }

    protected IEnumerator RemovePopupCoroutine()
    {
        if (animator != null)
        {
            animator.SetBool(isOpenText, false);

            while (!IsAnimatorFinished(animator, "Close"))
            {
                yield return null;
            }
        }

        var parentName = transform.parent.name;
        PopupManager.Instance.RemovePopup(parentName);

        yield break;
    }

    protected bool IsAnimatorFinished(Animator animator, string name)
    {
        var anim = animator.GetCurrentAnimatorStateInfo(0);
        return anim.IsName(name) && anim.normalizedTime >= 1f;
    }

    public string SetInfoText(string value) => infoText.text = value;
}
