using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FillAreaSlider : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Slider mySlider;

    public void OnPointerClick(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, 
            eventData.position, eventData.pressEventCamera, out localPoint))
        {
            float percent = Mathf.InverseLerp(rectTransform.rect.xMin, rectTransform.rect.xMax, localPoint.x);
            mySlider.value = percent * mySlider.maxValue;
        }
    }
}
