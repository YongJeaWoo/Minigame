using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StageEndAnyInputPanel : MonoBehaviour
{
    protected readonly string isOpenText = $"isOpen";

    [Header("정보 텍스트")]
    [SerializeField] private TextMeshProUGUI infoText;
    [Header("잡은 적의 수")]
    [SerializeField] private TextMeshProUGUI enemyCountText;

    private bool isInputKey = false;

    private Animator animator;

    public event Action OnAnyInputKey;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(isOpenText, true);
    }

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        if (Input.anyKeyDown && !isInputKey)
        {
            isInputKey = true;
            StartCoroutine(RemovePopupCoroutine());
        }
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
        OnAnyInputKey?.Invoke();

        PopupManager.Instance.RemovePopup(parentName);

        yield break;
    }

    protected bool IsAnimatorFinished(Animator animator, string name)
    {
        var anim = animator.GetCurrentAnimatorStateInfo(0);
        return anim.IsName(name) && anim.normalizedTime >= 1f;
    }

    public (string infoTextValue, string enemyCountTextValue) SetInfoText(string infoValue, string countValue, bool isCounting)
    {
        infoText.text = infoValue;
        enemyCountText.text = countValue;
        enemyCountText.gameObject.SetActive(isCounting);

        if (isCounting)
        {
            return (infoText.text, enemyCountText.text);
        }

        return (infoText.text, null);
    }
}
