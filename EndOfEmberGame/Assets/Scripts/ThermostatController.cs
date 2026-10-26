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

    private void OnEnable()
    {
        //setThermostatModeEvent.RegisterListener(setThermostatModeEvent);
    }

    private void OnDisable()
    {
        //setThermostatModeEvent.UnregisterListener(setThermostatModeEvent);
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

    public void SetThermostatMode(bool t)
    {
        coolingModeOutlineObj.SetActive(t);
        heatingModeOutlineObj.SetActive(!t);
        thermostatAudioSource.PlayOneShot(clipButtonPress);

        playerData.coolMode = t;
    }
}
