using UnityEngine;
using Proselyte.Sigils;
using UnityEngine.UI;

public class PowerboxController : MonoBehaviour
{
    public AudioSource powerboxAudioSource;
    public AudioClip clipPowerboxOpen;
    public AudioClip clipPowerboxClose;
    public AudioClip clipPowerboxFixed;

    public GameObject powerboxOpenObj;
    public GameObject powerboxCloseObj;

    public GameEvent powerboxOpenEvent;
    public GameEvent powerboxCloseEvent;
    public GameEvent powerboxFixedEvent;
    public GameEvent powerOutageEvent;
    public GameEvent powerRestoredEvent;

    public Button buttonBoxOpened;
    public Button buttonBoxClosed;

    public PlayerSO playerData;
    public SubtitlesSO subtitleData;

    public OnHover hoverPowerboxOpened;
    public OnHover hoverPowerboxClosed;

    private void OnEnable()
    {
        powerOutageEvent.RegisterListener(OnPowerOutage);
        buttonBoxClosed.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(22));
        hoverPowerboxClosed.cursorHoverType = CursorSpriteType.Inspect;
    }

    private void OnPowerOutage()
    {
        buttonBoxClosed.onClick.RemoveAllListeners();
        buttonBoxClosed.onClick.AddListener(OnPowerBoxOpen);
        buttonBoxOpened.onClick.AddListener(OnPowerBoxFixed);
        hoverPowerboxClosed.cursorHoverType = CursorSpriteType.HandOpen;
    }

    private void OnPowerBoxOpen()
    {
        powerboxCloseObj.SetActive(false);
        powerboxOpenObj.SetActive(true);
        powerboxAudioSource.PlayOneShot(clipPowerboxOpen);

        playerData.powerboxOpen = true;
    }

    private void OnPowerBoxClose()
    {
        powerboxCloseObj.SetActive(true);
        powerboxOpenObj.SetActive(false);
        powerboxAudioSource.PlayOneShot(clipPowerboxClose);

        playerData.powerboxOpen = false;
        buttonBoxClosed.onClick.RemoveAllListeners();
        buttonBoxClosed.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(23));
        hoverPowerboxClosed.cursorHoverType = CursorSpriteType.Inspect;
    }

    private void OnPowerBoxFixed()
    {
        powerboxAudioSource.PlayOneShot(clipPowerboxFixed);

        // TODO: affect the lighting in the room

        playerData.powerboxFixed = true;
        powerRestoredEvent.Raise();
        buttonBoxOpened.onClick.RemoveAllListeners();
        buttonBoxOpened.onClick.AddListener(OnPowerBoxClose);
    }
}
