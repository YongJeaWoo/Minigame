using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [Header("로딩 이미지")]
    [SerializeField] private Image loadingFillImage;
    [Header("로딩 텍스트")]
    [SerializeField] private TextMeshProUGUI loadingText;
    [Header("로딩 퍼센트 텍스트")]
    [SerializeField] private TextMeshProUGUI percentText;

    private static string nextSceneName;
    private const string loadingSceneName = "Loading";

    private float fakeProgress = 0f;

    private bool isAlmostDone = false;
    private bool isLoadingAnimation = true;

    public static void LoadScene(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene(loadingSceneName);
    }

    private void Start()
    {
        StartCoroutine(DelayStartCoroutine());
        StartCoroutine(LoadingTextAnimationCoroutine());
    }

    private IEnumerator DelayStartCoroutine()
    {
        // TODO : UIManager가 파괴되지 않는 경우 GUI 자체를 끄는 곳을 여기서 수행 해야 함

        float delayTime = Mathf.Lerp(1.0f, 1.5f, Mathf.InverseLerp(0f, 1f, fakeProgress)); 
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(LoadAsync(nextSceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        yield return new WaitForEndOfFrame();

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float totalTime = 5f;
        float elapsedTime = 0f;

        while (!op.isDone)
        {
            float realProgress = Mathf.Clamp01(op.progress / 0.9f);

            elapsedTime += Time.unscaledDeltaTime;
            float progressTimeRatio = Mathf.Clamp01(elapsedTime / totalTime);

            fakeProgress = Mathf.SmoothStep(0f, 1f, EaseInOut(progressTimeRatio));
            fakeProgress = Mathf.Min(fakeProgress, realProgress);

            loadingFillImage.fillAmount = fakeProgress;

            string pText = $"{Mathf.FloorToInt(fakeProgress * 100):D2}%";
            percentText.text = pText;

            if (fakeProgress >= 0.99f && !isAlmostDone)
            {
                isAlmostDone = true;
                isLoadingAnimation = false;  // 애니메이션 중지
            }

            if (fakeProgress >= 1f)
            {
                fakeProgress = 1f;
                loadingText.text = "로딩 완료!";
                percentText.text = "100%";

                yield return new WaitForSeconds(1f);

                op.allowSceneActivation = true;

                // TODO : UIManager가 파괴되지 않는 경우 GUI 자체를 키는 곳을 여기서 수행 해야 함
            }

            yield return null;
        }
    }

    private float EaseInOut(float t)
    {
        return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
    }

    private IEnumerator LoadingTextAnimationCoroutine()
    {
        string baseText = $"로딩 중";
        int dotCount = 0;

        while (isLoadingAnimation)
        {
            dotCount = (dotCount + 1) % 4;

            loadingText.text = baseText + new string('.', dotCount);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
