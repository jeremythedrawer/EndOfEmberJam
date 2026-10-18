using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Custom / Audio SO")]
public class AudioSO : ScriptableObject
{
    public float soundEffectsVolume;

    public AudioClip cursorClick;
    public AudioClip cursorHover;

    public AudioClip lightSwitch;
}
