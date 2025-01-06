using TMPro;
using UnityEngine;

public class InfoPanel : InteractionPanel
{
    [Header("정보 텍스트")]
    [SerializeField] private TextMeshProUGUI infoText;

    public string SetInfoText(string value) => infoText.text = value;
}
