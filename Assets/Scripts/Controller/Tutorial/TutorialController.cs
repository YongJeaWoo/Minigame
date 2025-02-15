using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialMessageBoxPrefab;
    [SerializeField] private Transform canvas;

    [System.Serializable]
    private class TutorialStep
    {
        [TextArea(4, 5)]
        public string message;
        public RectTransform uiTarget;
        public E_TutorialLocation location;
        public float bufferOffset;
    }

    [SerializeField] private List<TutorialStep> tutorialStepes = new List<TutorialStep>();

    private RectTransform canvasRect;

    private int currentStepIndex = 0;

    public event Action OnTutorialCompleted;
    
    public void TutorialStart()
    {
        canvasRect = canvas.GetComponent<RectTransform>();

        if (tutorialStepes.Count > 0)
        {
            ShowTutorialStep(currentStepIndex);
        }
    }

    private void ShowTutorialStep(int index)
    {
        if (index < 0 || index >= tutorialStepes.Count) return;

        var step = tutorialStepes[index];
        var message = SpawnTutorialObject();
        
        if (step.uiTarget != null)
        {
            message.InitializeMessageWithScreenPos(step.message, canvasRect, step.uiTarget.position, 
                step.location, step.bufferOffset);
        }

        message.OnMessageClosed += (sender, args) => 
        {
            ShowNextStep(); 

            if (currentStepIndex >= tutorialStepes.Count)
            {
                OnTutorialCompleted?.Invoke();
            }
        };
    }

    private TutorialMessageBox SpawnTutorialObject()
    {
        if (tutorialMessageBoxPrefab == null)
        {
            Debug.LogError("tutorialMessageBoxPrefab is null!");
            return null;
        }

        var obj = Instantiate(tutorialMessageBoxPrefab, canvas);
        if (obj == null)
        {
            Debug.LogError("Failed to instantiate tutorialMessageBoxPrefab!");
            return null;
        }

        var tutorialMessageBox = obj.GetComponent<TutorialMessageBox>();
        if (tutorialMessageBox == null)
        {
            Debug.LogError("TutorialMessageBox component is missing on the prefab!");
            return null;
        }

        return tutorialMessageBox;
    }

    public void ShowNextStep()
    {
        currentStepIndex++;

        if (currentStepIndex < tutorialStepes.Count)
        {
            ShowTutorialStep(currentStepIndex);
        }
    }
}
