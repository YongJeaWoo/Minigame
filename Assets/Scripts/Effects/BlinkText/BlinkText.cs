using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    [SerializeField] private float blinkDuration = 0.5f;
    private Coroutine blinkCoroutine;
    private TextMeshProUGUI myText;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        blinkCoroutine = StartCoroutine(TextBlinkCoroutine());
    }

    private void OnDisable()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
    }

    private IEnumerator TextBlinkCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(FadeText(1f, 0f));
            yield return StartCoroutine(FadeText(0f, 1f));
        }
    }

    private IEnumerator FadeText(float start, float end)
    {
        float timer = 0;
        Color textColor = myText.color;

        while (timer < blinkDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, timer / blinkDuration);
            myText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }

        myText.color = new Color(textColor.r, textColor.g, textColor.b, end);
    }
}
