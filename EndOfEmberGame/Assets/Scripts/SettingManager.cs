using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] Slider masterVolume;
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider SFXVolume;
    [SerializeField] Slider brightness;
    [SerializeField] Slider cursorSize;
    [SerializeField] Slider subtitleSpeed;
    [SerializeField] GameObject returnToStartButton;

    void Start()
    {
        masterVolume.value = SettingSO.masterVolume;
        musicVolume.value = SettingSO.musicVolume;
        SFXVolume.value = SettingSO.SFXVolume;
        brightness.value = SettingSO.brightness;
        cursorSize.value = SettingSO.cursorSize;
        subtitleSpeed.value = SettingSO.subtitleSpeed;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            returnToStartButton.SetActive(false);
        }
    }
    public void SetMasterVolume(float value)
    {
        SettingSO.masterVolume = value;
    }
    public void SetMusicVolume(float value)
    {
        SettingSO.musicVolume = value;
    }
    public void SetSFXVolume(float value)
    {
        SettingSO.SFXVolume = value;
    }
    public void SetBrightness(float value)
    {
        SettingSO.brightness = value;
    }
    public void SetCursorSize(float value)
    {
        SettingSO.cursorSize = (int)value;
    }
    public void SetSubtitleSpeed(float value)
    {
        SettingSO.subtitleSpeed = value;
    }

    void OnDisable()
    {
        SettingSO.ApplySetting();
    }

    public void CloseSetting()
    {
        SceneManager.UnloadSceneAsync(2);
    }

}
