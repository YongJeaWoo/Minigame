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

    private string GetTompostParentName(int target)
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
        int level = LevelDataManager.Instance.GetLevel(upgradeData);
        levelText.text = $"Level : {level + 1}";
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
        LevelDataManager.Instance.LevelUp(upgradeData);
        upgradeData.ApplyUpgrade();
        Time.timeScale = 1;
        PopupManager.Instance.RemovePopup(GetTompostParentName(3));
    }
}
