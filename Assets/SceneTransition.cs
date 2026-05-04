using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;

    public Image fadePanel;
    public float fadeDuration = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void TransitionTo(Vector3 targetPosition, Transform player)
    {
        StartCoroutine(FadeOutAndIn(targetPosition, player));
    }

    IEnumerator FadeOutAndIn(Vector3 targetPosition, Transform player)
    {

        yield return StartCoroutine(FadeTo(1f));

        player.position = targetPosition;

        yield return new WaitForSeconds(0.2f);


        yield return StartCoroutine(FadeTo(0f));
    }

    IEnumerator FadeIn()
    {
        fadePanel.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(FadeTo(0f));
    }

    IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = fadePanel.color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }

        fadePanel.color = new Color(0, 0, 0, targetAlpha);
    }
}