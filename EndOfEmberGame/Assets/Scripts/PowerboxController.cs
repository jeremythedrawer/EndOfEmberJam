using UnityEngine;
using Proselyte.Sigils;

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

    public PlayerSO playerData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable() 
    {
        powerboxOpenEvent.RegisterListener(OnPowerboxOpen);
        powerboxCloseEvent.RegisterListener(OnPowerboxClose);
    }

    private void OnDistable()
    {
        powerboxOpenEvent.UnregisterListener(OnPowerboxOpen);
        powerboxCloseEvent.UnregisterListener(OnPowerboxClose);
    }

    public void OnPowerboxOpen()
    {
        powerboxCloseObj.SetActive(false);
        powerboxOpenObj.SetActive(true);
        powerboxAudioSource.PlayOneShot(clipPowerboxOpen);

        playerData.powerboxOpen = true;
    }

    public void OnPowerboxClose()
    {
        powerboxCloseObj.SetActive(true);
        powerboxOpenObj.SetActive(false);
        powerboxAudioSource.PlayOneShot(clipPowerboxClose);

        playerData.powerboxOpen = false;
    }

    public void OnPowerBoxFixed()
    {
        powerboxAudioSource.PlayOneShot(clipPowerboxFixed);

        playerData.powerboxFixed = true;
    }
}
