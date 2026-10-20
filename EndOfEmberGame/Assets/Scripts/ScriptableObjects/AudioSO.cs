using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Custom / Audio SO")]
public class AudioSO : ScriptableObject
{
    public float soundEffectsVolume;

    public AudioClip cursorClick;
    public AudioClip cursorHover;

    public AudioClip lightSwitch;

    public AudioClip petriDishDrop001;
    public AudioClip petriDishDrop002;
    public AudioClip petriDishHover001;

    public AudioClip microscopeView001;
    public float[] microScopeClickTimes;
}
