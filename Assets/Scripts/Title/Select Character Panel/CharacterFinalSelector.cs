using UnityEngine;

public class CharacterFinalSelector : MonoBehaviour
{
    [SerializeField] private SelectCharacterPanel[] characterPanels;
    [SerializeField] private TitleController titleController;

    private PlayerObjectData selectedCharacter;
    private SelectCharacterPanel selectedPanel;

    private void Start()
    {
        InitSelectPanel();
    }

    private void InitSelectPanel()
    {
        foreach (var panel in characterPanels)
        {
            panel.SetSelected(this);
            panel.ResetColor();
        }

        if (characterPanels.Length > 0)
        {
            SetSelectedCharacter(characterPanels[0].GetPlayerData(), characterPanels[0]);
        }
    }

    private void Update()
    {
        HandlerInput();
    }

    private void HandlerInput()
    {
        if (characterPanels.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            ChangeSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSelection(-1);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedCharacter != null)
            {
                titleController.GoInGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            titleController.SetAnyKeyDown(false);
            var isOn = titleController.GetAnyKeyDown();
            titleController.TitleControlObject(isOn);
        }
    }

    private void ChangeSelection(int direction)
    {
        int currentIndex = System.Array.IndexOf(characterPanels, selectedPanel);

        if (currentIndex == -1) return;

        int newIndex = (currentIndex + direction + characterPanels.Length) % characterPanels.Length;

        SetSelectedCharacter(characterPanels[newIndex].GetPlayerData(), characterPanels[newIndex]);
    }

    public void SetSelectedCharacter(PlayerObjectData character, SelectCharacterPanel panel)
    {
        if (selectedPanel != null)
        {
            selectedPanel.ResetColor();
        }

        selectedPanel = panel;
        selectedPanel.HighlightSelected();
        selectedCharacter = character;
    }


    public SelectCharacterPanel GetSelectedCharacterPanel() => selectedPanel;
    public PlayerObjectData GetSelectedCharacter() => selectedCharacter;
}
