using Proselyte.Sigils;
using System.Collections;
using TMPro;
using UnityEngine;

public class SubtitleController : MonoBehaviour
{
    public InputSO inputData;
    public SubtitlesSO subtitlesData;
    public TMP_Text subtitlesTextTMP;
    public GameObject subtitlesOverlayCanvasGO;
    public AudioSource audioSource;
    public AudioClip audioClipPrintCharacter;

    public GameEvent triggerSubtitlesEvent;
    public GameEvent cursorHideEvent;
    public GameEvent cursorShowEvent;
    public GameEvent interactionEnableEvent;
    public GameEvent interactionDisableEvent;
    public GameEvent subtitleConfirmEvent;

    public float charPrintTimeWord = 0.075f;
    public float charPrintTimeSpace = 0.075f;
    public float charPrintTimeFullStop = 0.15f;
    public float charPrintTimeNewLine = 0.5f;
    public float charPrintPitchOffset = 0.3f;
    private float charPrintTimeScale = 1;

    private bool typingSubtitles = false;

    private WaitForSeconds waitForSecondsWord;
    private WaitForSeconds waitForSecondsSpace;
    private WaitForSeconds waitForSecondsFullStop;
    private Coroutine currentSubtitlesCoroutine;

    private string currentSubtitleString = string.Empty;
    private int currentSubtitleLine = 0;

    private float audioSourceStartPitch;

    private void Awake()
    {
        waitForSecondsWord = new WaitForSeconds(charPrintTimeWord * charPrintTimeScale);
        waitForSecondsSpace = new WaitForSeconds(charPrintTimeSpace * charPrintTimeScale);
        waitForSecondsFullStop = new WaitForSeconds(charPrintTimeFullStop * charPrintTimeScale);
    }

    private void Start()
    {
        audioSourceStartPitch = audioSource.pitch;
    }

    private void OnEnable()
    {
        triggerSubtitlesEvent.RegisterListener(OnTriggerSubtitles);
        subtitleConfirmEvent.RegisterListener(OnConfirmSubtitles);
        SettingSO.onApplySetting += UpdateSpeed;
    }

    private void OnDisable()
    {
        triggerSubtitlesEvent.UnregisterListener(OnTriggerSubtitles);
        subtitleConfirmEvent.UnregisterListener(OnConfirmSubtitles);
        SettingSO.onApplySetting -= UpdateSpeed;
    }

    public void OnTriggerSubtitles()
    {
        subtitlesData.displayingSubtitles = true;
        cursorHideEvent.Raise();
        interactionDisableEvent.Raise();
        subtitlesOverlayCanvasGO.SetActive(true);
        if(currentSubtitlesCoroutine != null) StopCoroutine(currentSubtitlesCoroutine);
        currentSubtitlesCoroutine = StartCoroutine(PrintSubtitles());
    }

    public void OnConfirmSubtitles()
    {
        if(!subtitlesData.displayingSubtitles)
        {
            return;
        }

        if(typingSubtitles && currentSubtitlesCoroutine != null)
        {
            // skipping animation: stop coroutine, complete message
            StopCoroutine(currentSubtitlesCoroutine);
            subtitlesTextTMP.text = currentSubtitleString;
            audioSource.PlayOneShot(audioClipPrintCharacter);
            
            typingSubtitles = false;
        }
        else
        {
            currentSubtitleLine++;
            if(currentSubtitleLine < subtitlesData.subtitles[subtitlesData.currentSubtitleIndex].subtitleLines.Length)
            {
                // print next subtitle line
                currentSubtitlesCoroutine = StartCoroutine(PrintSubtitles());
            }
            else
            {
                // subtitles completed
                subtitlesTextTMP.text = string.Empty;
                currentSubtitleLine = 0;
                subtitlesOverlayCanvasGO.SetActive(false);
                cursorShowEvent.Raise();
                interactionEnableEvent.Raise();
                subtitlesData.displayingSubtitles = false;
            }
        }
    }

    private IEnumerator PrintSubtitles()
    {
        typingSubtitles = true;
        currentSubtitleString = subtitlesData.subtitles[subtitlesData.currentSubtitleIndex].subtitleLines[currentSubtitleLine];
        subtitlesTextTMP.text = string.Empty;

        float pitchOffsetLower = audioSourceStartPitch - charPrintPitchOffset;
        float pitchOffsetUpper = audioSourceStartPitch + charPrintPitchOffset;

        int ctr = 0;
        int charTarget = currentSubtitleString.Length;

        // print characters one at a time with audio
        while(ctr < charTarget)
        {
            subtitlesTextTMP.text = currentSubtitleString[..ctr];
            audioSource.pitch = Random.Range(pitchOffsetLower, pitchOffsetUpper);
            audioSource.PlayOneShot(audioClipPrintCharacter);

            if(currentSubtitleString[ctr] == ' ')
            {
                yield return waitForSecondsSpace;
            }
            else if(currentSubtitleString[ctr] == '.')
            {
                yield return waitForSecondsFullStop;
            }
            else
            {
                yield return waitForSecondsWord;
            }
            ctr++;
        }

        subtitlesTextTMP.text = currentSubtitleString;
        typingSubtitles = false;
    }

    void UpdateSpeed()
    {
        charPrintTimeScale = 2f-SettingSO.subtitleSpeed;
        Awake();
    }
}
