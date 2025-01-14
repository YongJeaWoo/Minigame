using TMPro;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject selectCharacterPanel;
    [SerializeField] private CharacterFinalSelector characterFinalSelector;

    private bool isAnyKeyDown = false;

    private void Update()
    {
        AnyInputKey();
    }

    #region Title Panel Active
    private void AnyInputKey()
    {
        if (Input.anyKeyDown)
        {
            isAnyKeyDown = true;
            TitleControlObject(isAnyKeyDown);
            if (isAnyKeyDown) return;
        }
    }


    public void TitleControlObject(bool isOn)
    {
        titleText.gameObject.SetActive(!isOn);
        selectCharacterPanel.SetActive(isOn);
    }

    public void GoInGame()
    {
        var character = characterFinalSelector.GetSelectedCharacter();

        PlayerManager.Instance.SetPlayer(character);
        LoadingManager.LoadScene("Game");
    }

    public void ReturnSelectPanel()
    {
        isAnyKeyDown = false;
        characterFinalSelector.GetSelectedCharacterPanel().ResetColor();
        TitleControlObject(isAnyKeyDown);
    }
    #endregion

    public bool SetAnyKeyDown(bool isOn) => isAnyKeyDown = isOn;
    public bool GetAnyKeyDown() => isAnyKeyDown;
}
