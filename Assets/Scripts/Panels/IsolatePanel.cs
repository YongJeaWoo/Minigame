using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class IsolatePanel : MonoBehaviour
{
    protected readonly string isOpenText = $"isOpen";

    [SerializeField] private TextMeshProUGUI infoText;

    protected Animator animator;

    private void Awake()
    {
        InitValues();
    }

    private void Start()
    {
        AutoBehaviourPanel();
    }

    private void AutoBehaviourPanel()
    {
        StartCoroutine(AutoCloseCoroutine());
    }

    private IEnumerator AutoCloseCoroutine()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(RemovePopupCoroutine());
    }

    private void InitValues()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(isOpenText, true);
    }

    private IEnumerator RemovePopupCoroutine()
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
    }

    protected bool IsAnimatorFinished(Animator animator, string name)
    {
        var anim = animator.GetCurrentAnimatorStateInfo(0);
        return anim.IsName(name) && anim.normalizedTime >= 1f;
    }

    public string SetInfoText(string value) => infoText.text = value;
}
