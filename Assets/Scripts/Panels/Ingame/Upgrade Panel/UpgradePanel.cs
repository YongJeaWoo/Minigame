using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [Header("누를 버튼")]
    [SerializeField] private KeyCode pressKeyCode;

    [Space(5)]
    [Header("패널 정보")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI explainText;

    private UpgradeData upgradeData;

    private bool isUpgrade = false;

    private string GetTopmostParentName(int target)
    {
        Transform currentTransform = transform;
        int depth = 0;

        while (currentTransform.parent != null && depth < target)
        {
            currentTransform = currentTransform.parent;
            depth++;
        }

        return currentTransform.name;
    }

    public void InitData(UpgradeData upgradeData)
    {
        this.upgradeData = upgradeData;
        isUpgrade = false;
        int level = LevelDataManager.Instance.GetLevel(upgradeData);
        levelText.text = $"Level : {level}";
        iconImage.sprite = upgradeData.Icon;
        upgradeText.text = upgradeData.UpgradeText;
        typeText.text = upgradeData.GetTypeText();
        explainText.text = upgradeData.GetExplainText();
    }

    private void Update()
    {
        PressKey();
    }

    private void PressKey()
    {
        if (Input.GetKeyDown(pressKeyCode))
        {
            PerformAction();
        }
    }

    public void PerformAction()
    {
        if (isUpgrade) return;

        isUpgrade = true;
        LevelDataManager.Instance.LevelUp(upgradeData);
        upgradeData.ApplyUpgrade();
        Time.timeScale = 1;
        PopupManager.Instance.RemovePopup(GetTopmostParentName(3));
    }
}
