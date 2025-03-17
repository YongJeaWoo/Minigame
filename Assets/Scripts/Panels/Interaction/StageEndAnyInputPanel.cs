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
    [SerializeField] private GameObject tryButton;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(isOpenText, true);
    }

    public void RetryGame()
    {
        PlayerManager.Instance.ApplyUpgradeToPlayer(PlayerManager.Instance.GetPlayer());
        StartCoroutine(RemovePopupCoroutine("Game"));
    }

    public void ExitTitleGame()
    {
        StartCoroutine(RemovePopupCoroutine("Title"));
    }

    protected IEnumerator RemovePopupCoroutine(string sceneName)
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

        AudioManager.Instance.StopBGM();
        PopupManager.Instance.RemovePopup(parentName);
        LoadingManager.LoadScene(sceneName);
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
            tryButton.SetActive(true);
            return (infoText.text, enemyCountText.text);
        }

        tryButton.SetActive(false);
        return (infoText.text, null);
    }
}
