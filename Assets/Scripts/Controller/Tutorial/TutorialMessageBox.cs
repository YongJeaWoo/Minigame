using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum E_TutorialLocation
{
    POINT_TO_TOP,
    POINT_TO_BOTTOM,
    POINT_TO_LEFT,
    POINT_TO_RIGHT
}

public class TutorialMessageBox : MonoBehaviour
{
    public EventHandler OnMessageClosed;

    [Header("리소스들")]
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform wrapper;
    [SerializeField] private RectTransform topIcon;
    [SerializeField] private RectTransform bottomIcon;
    [SerializeField] private RectTransform leftIcon;
    [SerializeField] private RectTransform rightIcon;

    [Header("세팅")]
    [SerializeField] private float heightPlus;
    [SerializeField] private float widthPlus;

    private const float buttonOffset = 60f;
    private RectTransform textRect;

    public void CloseTutorialMessage()
    {
        OnMessageClosed?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    private void Awake()
    {
        textRect = tutorialText.rectTransform;
    }

    private void OnDestroy()
    {
        OnMessageClosed = null; 
    }

    public void InitializeMessageWithScreenPos(string message, RectTransform canvasRect, Vector3 screenPos,
    E_TutorialLocation locationType, float bufferOffset, Sprite iconReplacement = null)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out var localPoint);
        InitializeMessageWithRectTransform(message, canvasRect, localPoint, locationType, bufferOffset, iconReplacement);
    }

    private void InitializeMessageWithRectTransform(string message, RectTransform canvasRect, Vector2 localRectPoint,
        E_TutorialLocation locationType, float bufferOffset, Sprite iconReplacement = null)
    {
        tutorialText.text = message;
        if (iconReplacement != null)
        {
            switch (locationType)
            {
                case E_TutorialLocation.POINT_TO_TOP:
                    topIcon.GetComponent<Image>().sprite = iconReplacement;
                    break;
                case E_TutorialLocation.POINT_TO_BOTTOM:
                    bottomIcon.GetComponent<Image>().sprite = iconReplacement;
                    break;
                case E_TutorialLocation.POINT_TO_LEFT:
                    leftIcon.GetComponent<Image>().sprite = iconReplacement;
                    break;
                case E_TutorialLocation.POINT_TO_RIGHT:
                    rightIcon.GetComponent<Image>().sprite = iconReplacement;
                    break;
            }
        }

        tutorialText.ForceMeshUpdate();
        LayoutRebuilder.ForceRebuildLayoutImmediate(textRect);

        var centerRectOffset = UpdateLayout(locationType, bufferOffset);

        wrapper.anchoredPosition = ClampToScreen(localRectPoint - centerRectOffset, canvasRect);
    }

    private Vector2 ClampToScreen(Vector2 position, RectTransform canvasRect)
    {
        Vector2 minBounds = new Vector2(background.rect.width / 2, background.rect.height / 2);
        Vector2 maxBounds = new Vector2(canvasRect.rect.width / 2 - minBounds.x, canvasRect.rect.height / 2 - minBounds.y);

        float clampedX = Mathf.Clamp(position.x, -maxBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(position.y, -maxBounds.y, maxBounds.y);

        return new Vector2(clampedX, clampedY);
    }

    private Vector2 UpdateLayout(E_TutorialLocation locationType, float bufferOffset)
    {
        var result = new Vector2();
        var bgRect = new Vector2(textRect.rect.width + widthPlus,
            textRect.rect.height + heightPlus + buttonOffset);
        background.sizeDelta = bgRect;

        switch (locationType)
        {
            case E_TutorialLocation.POINT_TO_TOP:
                topIcon.gameObject.SetActive(true);
                topIcon.anchoredPosition = new Vector2(0, bgRect.y / 2 + topIcon.sizeDelta.y / 2 - buttonOffset / 2);
                result = new Vector2(0, bgRect.y / 2 + topIcon.sizeDelta.y - buttonOffset / 2 + bufferOffset);
                break;
            case E_TutorialLocation.POINT_TO_BOTTOM:
                bottomIcon.gameObject.SetActive(true);
                bottomIcon.anchoredPosition = new Vector2(0, -bgRect.y / 2 - bottomIcon.sizeDelta.y / 2 - buttonOffset / 2);
                result = new Vector2(0, -bgRect.y / 2 - bottomIcon.sizeDelta.y - buttonOffset / 2 - bufferOffset);
                break;
            case E_TutorialLocation.POINT_TO_LEFT:
                leftIcon.gameObject.SetActive(true);
                leftIcon.anchoredPosition = new Vector2(-bgRect.x / 2 - leftIcon.sizeDelta.x / 2, -buttonOffset / 2);
                result = new Vector2(-bgRect.x / 2 - leftIcon.sizeDelta.x - bufferOffset, -buttonOffset / 2);
                break;
            case E_TutorialLocation.POINT_TO_RIGHT:
                rightIcon.gameObject.SetActive(true);
                rightIcon.anchoredPosition = new Vector2(bgRect.x / 2 + rightIcon.sizeDelta.x / 2, -buttonOffset / 2);
                result = new Vector2(bgRect.x / 2 + rightIcon.sizeDelta.x + bufferOffset, -buttonOffset / 2);
                break;
        }

        return result;
    }
}