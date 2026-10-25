using Proselyte.Sigils;
using System.Collections;
using TMPro;
using UnityEngine;

public class SubtitleController : MonoBehaviour
{
    // what should this do?

    // show unique dialogue.

    // dialogue should need to be skipped by the player by clicking.
    // dialogue should appear slowly.

    //DialogueSO dialogueData;

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

    public float charPrintTimeWord = 0.075f;
    public float charPrintTimeSpace = 0.1f;
    public float charPrintTimeNewLine = 0.5f;
    public float charPrintPitchOffset = 0.3f;

    private bool typingSubtitles = false;
    private WaitForSeconds waitForSecondsWord;
    private WaitForSeconds waitForSecondsSpace;
    private Coroutine currentSubtitlesCoroutine;
    private string currentSubtitle;

    private float audioSourceStartPitch;

    private const char newLineChar = '#';

    private void Awake()
    {
        waitForSecondsWord =  new WaitForSeconds(charPrintTimeWord);
        waitForSecondsSpace = new WaitForSeconds(charPrintTimeSpace);
    }

    private void Start()
    {
        audioSourceStartPitch = audioSource.pitch;
    }

    private void OnEnable()
    {
        triggerSubtitlesEvent.RegisterListener(OnTriggerSubtitles);
    }

    private void OnDisable()
    {
        triggerSubtitlesEvent.UnregisterListener(OnTriggerSubtitles);
    }

    public void OnTriggerSubtitles()
    {
        cursorHideEvent.Raise();
        interactionDisableEvent.Raise();
        subtitlesOverlayCanvasGO.SetActive(true);
        if(currentSubtitlesCoroutine != null) StopCoroutine(currentSubtitlesCoroutine);
        currentSubtitlesCoroutine = StartCoroutine(PrintSubtitles());
    }

    public void OnConfirmSubtitles()
    {
        if(typingSubtitles && currentSubtitlesCoroutine != null)
        {
            // skipping animation: stop coroutine, complete message
            StopCoroutine(currentSubtitlesCoroutine);
            subtitlesTextTMP.text = currentSubtitle;
            typingSubtitles = false;
            audioSource.PlayOneShot(audioClipPrintCharacter);
        }
        else
        {
            // confirm subtitles: clear message, show cursor again
            subtitlesTextTMP.text = string.Empty;
            subtitlesOverlayCanvasGO.SetActive(false);
            cursorShowEvent.Raise();
            interactionEnableEvent.Raise();
        }
        
    }

    private IEnumerator PrintSubtitles()
    {
        typingSubtitles = true;
        currentSubtitle = subtitlesData.subtitles[subtitlesData.currentSubtitleIndex];
        subtitlesTextTMP.text = string.Empty;

        float pitchOffsetLower = audioSourceStartPitch - charPrintPitchOffset;
        float pitchOffsetUpper = audioSourceStartPitch + charPrintPitchOffset;

        int ctr = 0;
        int charTarget = currentSubtitle.Length;

        while(ctr < charTarget)
        {
            subtitlesTextTMP.text = currentSubtitle[..ctr];
            audioSource.pitch = Random.Range(pitchOffsetLower, pitchOffsetUpper);
            audioSource.PlayOneShot(audioClipPrintCharacter);

            if(currentSubtitle[ctr] == ' ')
            {
                yield return waitForSecondsSpace;
            }
            else
            {
                yield return waitForSecondsWord;
            }
            ctr++;
        }

        subtitlesTextTMP.text = currentSubtitle;
        typingSubtitles = false;
    }
}
