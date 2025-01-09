using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeClass : MonoBehaviour
{
    [Header("화면 페이드 아웃")]
    [SerializeField] private Canvas fadeCanvas;

    private IEnumerator FadeCoroutine(float start, float end)
    {
        fadeCanvas.gameObject.SetActive(true);
        float duration = 1f;
        float elapsed = 0;

        var fadeImage = fadeCanvas.transform.GetChild(0).GetComponent<Image>();
        Color fadeColor = fadeImage.color;
        fadeColor.a = start;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeColor.a = Mathf.Lerp(start, end, elapsed / duration);
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = end;
        fadeImage.color = fadeColor;
    }

    public Coroutine FadeMethod(float start, float end)
    {
        return StartCoroutine(FadeCoroutine(start, end));
    }
}
