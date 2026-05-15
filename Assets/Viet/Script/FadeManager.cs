using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f; 

    public IEnumerator FadeOut() 
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = timer / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    public IEnumerator FadeIn() 
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = 1 - (timer / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}