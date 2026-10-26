using UnityEngine;
using TMPro;
using Proselyte.Sigils;

public class ThermostatController : MonoBehaviour
{
    public TextMeshProUGUI thermostatText;

    public AudioSource thermostatAudioSource;
    public AudioClip clipButtonPress;

    public GameObject coolingModeOutlineObj;
    public GameObject heatingModeOutlineObj;

    public GameEvent setThermostatModeEvent;
    
    public int currentRoomTemperature;
    private int maxRoomTemperature = 40;
    private int minRoomTemperature = 10;

    public PlayerSO playerData;

    private bool fanMode = true;

    void Start()
    {
        coolingModeOutlineObj.SetActive(fanMode);
        heatingModeOutlineObj.SetActive(!fanMode);
    }

    private void OnEnable()
    {
        setThermostatModeEvent.RegisterListener(SetThermostatMode);
    }

    private void OnDisable()
    {
        setThermostatModeEvent.UnregisterListener(SetThermostatMode);
    }

    public void IncreaseTemperature()
    {
        if (currentRoomTemperature < maxRoomTemperature)
        {
            currentRoomTemperature++;
        }

        thermostatAudioSource.PlayOneShot(clipButtonPress);
        thermostatText.text = currentRoomTemperature.ToString();
    }

    public void DecreaseTemperature()
    {
        if (currentRoomTemperature > minRoomTemperature)
        {
            currentRoomTemperature--;
        }

        thermostatAudioSource.PlayOneShot(clipButtonPress);
        thermostatText.text = currentRoomTemperature.ToString();
    }

    public void SetThermostatMode()
    {
        if (fanMode == false)
        {
            coolingModeOutlineObj.SetActive(true);
            heatingModeOutlineObj.SetActive(false);
            thermostatAudioSource.PlayOneShot(clipButtonPress);

            fanMode = true;
        }
        else
        {
            coolingModeOutlineObj.SetActive(false);
            heatingModeOutlineObj.SetActive(true);
            thermostatAudioSource.PlayOneShot(clipButtonPress);

            fanMode = false;
        }

        playerData.fanMode = fanMode;
    }
}
