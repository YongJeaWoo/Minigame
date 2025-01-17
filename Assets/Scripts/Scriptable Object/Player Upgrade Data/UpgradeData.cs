using UnityEngine;

public enum E_Type
{
    Module,
    Weapon
}

public abstract class UpgradeData : ScriptableObject
{
    [Header("메인 정보")]
    [SerializeField] private Sprite icon;
    [SerializeField] private string upgradeText;
    [TextArea(4, 5)]
    [SerializeField] protected string explainText;
    [SerializeField] private E_Type type;

    public Sprite Icon { get => icon;}
    public string UpgradeText { get => upgradeText; }
    public virtual string GetExplainText() => explainText;
    public string GetTypeText()
    {
        switch (type)
        {
            case E_Type.Module:
                return "모듈";  
            case E_Type.Weapon:
                return "무기"; 
            default:
                return "기타";  
        }
    }

    public abstract float GetValueForLevel(int level);
    public abstract void ApplyUpgrade();
}
