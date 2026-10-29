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

    public GameEvent SetCoolMode;
    public GameEvent SetHeatMode;
    
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

    public void IncreaseTemperature()
    {
        if (currentRoomTemperature < maxRoomTemperature)
        {
            currentRoomTemperature++;
        }

        thermostatAudioSource.PlayOneShot(clipButtonPress);
        thermostatText.text = currentRoomTemperature.ToString();

        playerData.currentRoomTemperature = currentRoomTemperature;
    }

    public void DecreaseTemperature()
    {
        if (currentRoomTemperature > minRoomTemperature)
        {
            currentRoomTemperature--;
        }

        thermostatAudioSource.PlayOneShot(clipButtonPress);
        thermostatText.text = currentRoomTemperature.ToString();

        playerData.currentRoomTemperature = currentRoomTemperature;
    }

    public void SetCoolingMode()
    {
        coolingModeOutlineObj.SetActive(true);
        heatingModeOutlineObj.SetActive(false);
        thermostatAudioSource.PlayOneShot(clipButtonPress);

        fanMode = true;

        playerData.fanMode = true;

        SetCoolMode.Raise();
    }

    public void SetHeatingMode()
    {
        coolingModeOutlineObj.SetActive(false);
        heatingModeOutlineObj.SetActive(true);
        thermostatAudioSource.PlayOneShot(clipButtonPress);

        fanMode = false;

        playerData.fanMode = false;

        SetHeatMode.Raise();
    }
}

