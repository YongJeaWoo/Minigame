using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] private GameObject titleCollection;
    [SerializeField] private GameObject selectCharacterPanel;
    [SerializeField] private GameObject publicUpgradePanel;
    [SerializeField] private CharacterFinalSelector characterFinalSelector;
    [SerializeField] private AudioClip titleClip;
    [SerializeField] private AudioClip buttonClickSound;

    private bool isStartButtonClicked = false;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(titleClip);
    }

    private void Update()
    {
        StartFirstTutorial();
    }

    private void StartFirstTutorial()
    {
        if (!PlayerManager.Instance.GetTutorials()[0] && publicUpgradePanel.activeSelf)
        {
            GetComponent<TutorialController>().TutorialStart();
            PlayerManager.Instance.SetTutorials(0);
        }
    }

    #region Title Panel Active
    public void StartButton()
    {
        if (!publicUpgradePanel.activeSelf)
        {
            isStartButtonClicked = true;
            AudioManager.Instance.PlaySFX(buttonClickSound);
            TitleControlObject(isStartButtonClicked);
        }
    }

    public void TitleControlObject(bool isOn)
    {
        selectCharacterPanel.SetActive(isOn);
        titleCollection.SetActive(!isOn);
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
        isStartButtonClicked = false;
        characterFinalSelector.GetSelectedCharacterPanel().ResetColor();
        TitleControlObject(isStartButtonClicked);
    }
    #endregion
    
    public bool SetAnyKeyDown(bool isOn) => isStartButtonClicked = isOn;
    public bool GetAnyKeyDown() => isStartButtonClicked;
}
