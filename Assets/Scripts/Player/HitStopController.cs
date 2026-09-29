using System.Collections;
using UnityEngine;

public class HitStopController : MonoBehaviour
{
    private Coroutine hitstopCoroutine;

    public void PlayHitstop(float duration)
    {
        if(hitstopCoroutine != null)
        {
            StopCoroutine(hitstopCoroutine);
        }

        hitstopCoroutine = StartCoroutine(HitStop(duration));
    }

    private IEnumerator HitStop(float duration)
    {
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1;

        hitstopCoroutine = null;
    }
}
