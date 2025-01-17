using UnityEngine;

public enum E_Type
{
    Module,
    Weapon
}

public abstract class UpgradeData : ScriptableObject
{
    [Header("���� ����")]
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
                return "���";  
            case E_Type.Weapon:
                return "����"; 
            default:
                return "��Ÿ";  
        }
    }

    public abstract float GetValueForLevel(int level);
    public abstract void ApplyUpgrade();
}
