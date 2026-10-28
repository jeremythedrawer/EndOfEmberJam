using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // rain severity
    public AudioMixer mixer;
    public AudioMixerGroup mixerGroup;
    public AudioSource ambienceAudioSource;
    public AudioSource heaterAudioSource;
    public PlayerSO playerData;

    public float rainFadeInTime = 1f;
    public float rainFadeOutTime = 1f;
    public float rainAtticFadeOutTime = 0.4f;
    
    public float lowPassAtticMin = 450f;
    public float lowPassRoomMin = 750f;
    public float lowPassRoomMax = 4500f;

    public float ambienceVolAttic = -15f;
    private float ambienceVolRoom = 0.00f;

    public float rainLowPassCursorPosition;
    
    private const string ambLowPassID = "ambience_low_pass";
    private const string ambVolID = "ambience_vol";
    private Coroutine rainCoroutine;

    private const string heaterLowPassID = "heater_low_pass";
    private const string heaterVolID = "heater_vol";
    private Coroutine heaterCoroutine;

    private void Start()
    {
        mixer.SetFloat(ambLowPassID, lowPassRoomMin);
        mixer.SetFloat(ambVolID, 0.00f);
        rainLowPassCursorPosition = lowPassRoomMin;
        ambienceAudioSource.Play();
    }

    public void RainFadeInRoom()
    {
        if(rainCoroutine != null) StopCoroutine(rainCoroutine);
        rainCoroutine = StartCoroutine(FadeInRainRoomAmbience());
    }

    public void RainFadeOutRoom()
    {
        if(rainCoroutine != null) StopCoroutine(rainCoroutine);
        rainCoroutine = StartCoroutine(FadeOutRainRoomAmbience());
    }

    public void RainFadeOutAttic()
    {
        if(rainCoroutine != null) StopCoroutine(rainCoroutine);
        rainCoroutine = StartCoroutine(FadeOutRainAtticAmbience());
    }

    public void RainFadeInAttic()
    {
        if(rainCoroutine != null) StopCoroutine(rainCoroutine);
        rainCoroutine = StartCoroutine(FadeInRainAtticAmbience());
    }

    public void HeaterRoomAmbience()
    {
        //if (heaterCoroutine != null) StopCoroutine(heaterCoroutine);
        //heaterCoroutine = StartCoroutine(FadeInHeaterRoomAmbience());

        if (playerData.fanMode == false)
        {
            heaterAudioSource.Play();
        }
        else
        {
            heaterAudioSource.Stop();
        }
    }

    // Opening window in room
    private IEnumerator FadeInRainRoomAmbience()
    {
        float ctr = 0;
        float cursorStartPos = rainLowPassCursorPosition;
        float remainingTime = 1 - Mathf.InverseLerp(lowPassRoomMin, lowPassRoomMax, cursorStartPos);
        remainingTime *= rainFadeInTime;

        while(ctr < remainingTime)
        {
            float v = Mathf.Lerp(0, 1, ctr / remainingTime);
            float t = Mathf.Lerp(cursorStartPos, lowPassRoomMax, v);
            rainLowPassCursorPosition = t;
            mixer.SetFloat(ambLowPassID, t);

            ctr += Time.deltaTime;
            yield return null;
        }

        rainLowPassCursorPosition = lowPassRoomMax;
        mixer.SetFloat(ambLowPassID, lowPassRoomMax);
    }

    // closing window in room
    private IEnumerator FadeOutRainRoomAmbience()
    {
        float ctr = 0;
        float cursorStartPos = rainLowPassCursorPosition;
        float remainingTime = 1 - Mathf.InverseLerp(lowPassRoomMax, lowPassRoomMin, cursorStartPos);
        remainingTime *= rainFadeOutTime;

        while(ctr < remainingTime)
        {
            float v = Mathf.Lerp(0, 1, ctr / remainingTime);
            float t = Mathf.Lerp(cursorStartPos, lowPassRoomMin, v);
            rainLowPassCursorPosition = t;
            mixer.SetFloat(ambLowPassID, t);

            ctr += Time.deltaTime;
            yield return null;
        }

        rainLowPassCursorPosition = lowPassRoomMin;
        mixer.SetFloat(ambLowPassID, lowPassRoomMin);
    }

    // going to attic
    private IEnumerator FadeOutRainAtticAmbience()
    {
        float ctr = 0;
        float cursorStartPos = rainLowPassCursorPosition;
        float targetPos = playerData.windowOpen ? lowPassRoomMax : lowPassRoomMin;
        float remainingTime = 1 - Mathf.InverseLerp(targetPos, lowPassAtticMin, cursorStartPos);
        remainingTime *= rainAtticFadeOutTime * 1.25f;

        while(ctr < remainingTime)
        {
            float v = Mathf.Lerp(0, 1, ctr / remainingTime);
            float t = Mathf.Lerp(cursorStartPos, lowPassAtticMin, v);
            rainLowPassCursorPosition = t;
            mixer.SetFloat(ambLowPassID, t);

            float vol = Mathf.Lerp(ambienceVolRoom, ambienceVolAttic, t);
            mixer.SetFloat(ambVolID, vol);

            ctr += Time.deltaTime;
            yield return null;
        }

        rainLowPassCursorPosition = lowPassAtticMin;
        mixer.SetFloat(ambLowPassID, lowPassAtticMin);
    }

    // going back to room from attic
    private IEnumerator FadeInRainAtticAmbience()
    {
        float ctr = 0;
        float cursorStartPos = rainLowPassCursorPosition;
        float targetPos = playerData.windowOpen ? lowPassRoomMax : lowPassRoomMin;
        float remainingTime = 1 - Mathf.InverseLerp(lowPassAtticMin, targetPos, cursorStartPos);
        remainingTime *= rainFadeInTime;

        while(ctr < remainingTime)
        {
            float v = Mathf.Lerp(0, 1, ctr / remainingTime);
            float t = Mathf.Lerp(cursorStartPos, targetPos, v);
            rainLowPassCursorPosition = t;
            mixer.SetFloat(ambLowPassID, t);

            float vol = Mathf.Lerp(ambienceVolAttic, ambienceVolRoom, t);
            mixer.SetFloat(ambVolID, vol);

            ctr += Time.deltaTime;
            yield return null;
        }

        rainLowPassCursorPosition = targetPos;
        mixer.SetFloat(ambLowPassID, targetPos);
    }
    
    private IEnumerator FadeInHeaterRoomAmbience()
    {
        if (playerData.fanMode == false)
        {
            heaterAudioSource.Play();
        }
        else
        {
            heaterAudioSource.Stop();
        }

        yield return null;
    }

    void OnEnable()
    {
        SettingSO.onApplySetting += UpdateVolume;
    }
    void OnDisable()
    {
        SettingSO.onApplySetting -= UpdateVolume;
    }

    void UpdateVolume()
    {
        mixer.SetFloat("MasterVolume", LinearToDec(SettingSO.masterVolume));
        mixer.SetFloat("MusicVolume", LinearToDec(SettingSO.musicVolume));
        mixer.SetFloat("SFXVolume", LinearToDec(SettingSO.SFXVolume));
    }

    float LinearToDec(float linear_value)
    {
        return 20 * Mathf.Log10(Mathf.Clamp(linear_value, 0.001f, 1));
    }
}

