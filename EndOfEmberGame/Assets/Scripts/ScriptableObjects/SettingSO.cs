using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "SettingSO", menuName = "ScriptableObjects / SettingSO")]
public class SettingSO : ScriptableObject
{
    public static float masterVolume = 1;
    public static float musicVolume = 1;
    public static float SFXVolume = 1;
    public static float subtitleSpeed = 1f;
    public static float brightness = 0f;
    public static int cursorSize = 2;
    public delegate void settingUpdateEvent();
    public static event settingUpdateEvent onApplySetting;

    public static void ApplySetting()
    {
        onApplySetting?.Invoke();
    }


}
