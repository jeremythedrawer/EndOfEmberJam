using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // rain severity
    public AudioMixer mixer;
    public AudioMixerGroup mixerGroup;
    public PlayerSO playerData;

    public float rainFadeInTime = 1f;
    public float rainFadeOutTime = 1f;
    public float lowPassMin = 650f;
    public float lowPassMax = 8000f;
    
    private const string ambLowPassID = "ambience_low_pass";
    private Coroutine rainCoroutine;


    private void Start()
    {
        mixer.SetFloat(ambLowPassID, lowPassMin);
    }

    public void RainFadeIn()
    {
        if(rainCoroutine != null) StopCoroutine(rainCoroutine);
        StartCoroutine(FadeInRainAmbience());
    }

    public void RainFadeOut()
    {
        if(rainCoroutine != null) StopCoroutine(rainCoroutine);
        rainCoroutine = StartCoroutine(FadeOutRainAmbience());
    }

    // TODO(Jazz): improve directionality of lerp for mid-coroutine redirections
    private IEnumerator FadeInRainAmbience()
    {
        float ctr = 0;
        while(ctr < rainFadeInTime)
        {
            float v = Mathf.Lerp(0, 1, ctr / rainFadeInTime);
            float t = Mathf.Lerp(lowPassMin, lowPassMax, v);
            mixer.SetFloat(ambLowPassID, t);

            ctr += Time.deltaTime;
            yield return null;
        }

        mixer.SetFloat(ambLowPassID, lowPassMax);
    }

    private IEnumerator FadeOutRainAmbience()
    {
        float ctr = 0;
        while(ctr < rainFadeOutTime)
        {
            float v = Mathf.Lerp(0, 1, ctr / rainFadeOutTime);
            float t = Mathf.Lerp(lowPassMax, lowPassMin, v);
            mixer.SetFloat(ambLowPassID, t);

            ctr += Time.deltaTime;
            yield return null;
        }

        mixer.SetFloat(ambLowPassID, lowPassMin);
    }
}
