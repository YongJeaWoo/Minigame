using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject selectCharacterPanel;
    [SerializeField] private GameObject publicUpgradePanel;
    [SerializeField] private CharacterFinalSelector characterFinalSelector;
    [SerializeField] private AudioClip titleClip;
    [SerializeField] private KeyCode[] inputKeys;

    private bool isAnyKeyDown = false;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(titleClip);
    }

    private void Update()
    {
        HandleTitleText();
        AnyInputKey();
    }

    #region Title Panel Active
    private void AnyInputKey()
    {
        if (!publicUpgradePanel.activeSelf)
        {
            foreach (var key in inputKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    if (Input.GetKeyDown(inputKeys[0]) && IsPointerOverUI())
                        return;

                    isAnyKeyDown = true;
                    TitleControlObject(isAnyKeyDown);
                    break;
                }
            }
        }
    }

    private void HandleTitleText()
    {
        titleText.gameObject.SetActive(!publicUpgradePanel.activeSelf && !isAnyKeyDown);
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
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
        AudioManager.Instance.StopBGM(1.5f);
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
