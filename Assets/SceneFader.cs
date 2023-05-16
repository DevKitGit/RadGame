using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1f;
    public string sceneName;
    public float delayTime = 5f;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // Start fully transparent
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        
        Invoke(nameof(SwitchScene),delayTime);
    }

    public void SwitchScene()
    {
        StartCoroutine(FadeAndSwitch(sceneName));
    }

    IEnumerator FadeAndSwitch(string sceneName)
    {
        // Fade to black
        yield return StartCoroutine(Fade(1f));

        // Load scene
        SceneManager.LoadScene(sceneName);

        // Fade to clear
        yield return StartCoroutine(Fade(0f));
    }

    IEnumerator Fade(float targetOpacity)
    {
        float fadeStart = fadeImage.color.a;
        float fadeProgress = 0f;

        while (fadeProgress < 1f)
        {
            fadeProgress += Time.deltaTime * fadeSpeed;

            float currentOpacity = Mathf.Lerp(fadeStart, targetOpacity, fadeProgress);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, currentOpacity);

            yield return null;
        }
    }
}