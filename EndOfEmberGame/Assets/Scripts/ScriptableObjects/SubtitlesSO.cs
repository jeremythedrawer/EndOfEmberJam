using Proselyte.Sigils;
using UnityEngine;

[CreateAssetMenu(fileName = "SubtitlesSO", menuName = "ScriptableObjects / SubtitlesSO")]
public class SubtitlesSO : ScriptableObject
{
    public GameEvent TriggerSubtitlesEvent;
    public string[] subtitles = {"I've already printed that page."};
    public int currentSubtitleIndex = -1;

    public void TriggerSubtitleFromIndex(int index)
    {
        currentSubtitleIndex = index;
        TriggerSubtitlesEvent.Raise();
    }
}
