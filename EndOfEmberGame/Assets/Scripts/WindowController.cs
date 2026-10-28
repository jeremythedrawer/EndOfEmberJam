using Proselyte.Sigils;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public AudioSource windowAudioSource;
    public AudioClip clipWindowOpen;
    public AudioClip clipWindowClose;
    
    public GameObject windowClosedObj;
    public GameObject windowOpenObj;

    public GameEvent windowOpenEvent;
    public GameEvent windowCloseEvent;

    public PlayerSO playerData;
    public SubtitlesSO subtitlesData;

    private void OnEnable()
    {
        windowOpenEvent.RegisterListener(OnWindowOpen);
        windowCloseEvent.RegisterListener(OnWindowClose);
    }

    private void OnDisable()
    {
        windowOpenEvent.UnregisterListener(OnWindowOpen);
        windowCloseEvent.UnregisterListener(OnWindowClose);
    }

    public void OnWindowOpen()
    {
        if(!playerData.windowOpenedAtLeastOnce)
        {
            subtitlesData.TriggerSubtitleFromIndex(12);
            playerData.windowOpenedAtLeastOnce = true;
        }
        windowClosedObj.SetActive(false);
        windowOpenObj.SetActive(true);
        windowAudioSource.PlayOneShot(clipWindowOpen);

        playerData.windowOpen = true;
    }

    public void OnWindowClose()
    {
        windowClosedObj.SetActive(true);
        windowOpenObj.SetActive(false);
        windowAudioSource.PlayOneShot(clipWindowClose);

        playerData.windowOpen = false;
    }
}
