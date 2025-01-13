using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCharacterPanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlayerObjectData playerData;
    [SerializeField] private Image panelImage;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI explainText;
    [SerializeField] private Color defaultTextColor;

    [SerializeField] private CharacterFinalSelector selector;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color selectedColor;

    private void Start()
    {
        InitData();
    }

    private void InitData()
    {
        characterImage.sprite = playerData.GetSelectedImage();
        explainText.text = playerData.GetExplain();
    }

    public void SetSelected(CharacterFinalSelector selector)
    {
        this.selector = selector;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selector?.SetSelectedCharacter(playerData, this);
    }

    public void HighlightSelected()
    {
        panelImage.color = selectedColor;
        explainText.color = Color.black;
    }

    public void ResetColor()
    {
        panelImage.color = defaultColor;
        explainText.color = defaultTextColor;
    }

    public PlayerObjectData GetPlayerData() => playerData;
}
