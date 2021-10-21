using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAfterDelay : MonoBehaviour
{
    [SerializeField]
    float delayBeforeFading = 2f;
    [SerializeField]
    float fadeTime = 0.25f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delayBeforeFading);
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup == null)
        {
            Debug.LogWarning("Cannot fade - no canvas group attached!");
            yield break;
        }

        if(fadeTime <= 0)
        {
            yield break;
        }

        var fadeTimeElapsed = 0f;
        while(fadeTimeElapsed < fadeTime)
        {
            fadeTimeElapsed += Time.deltaTime;
            var t = fadeTimeElapsed / fadeTime;
            var alpha = 1f - t;
            canvasGroup.alpha = alpha;

            yield return null;
        }

        Destroy(gameObject);
    }
}
