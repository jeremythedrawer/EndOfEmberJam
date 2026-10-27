using Proselyte.Sigils;
using UnityEngine;

[CreateAssetMenu(fileName = "SubtitlesSO", menuName = "ScriptableObjects / SubtitlesSO")]
public class SubtitlesSO : ScriptableObject
{
    [System.Serializable]
    public struct SubtitleData
    {
        [TextArea(2,4)]
        public string[] subtitleLines;
    }

    public GameEvent TriggerSubtitlesEvent;
    public SubtitleData[] subtitles;
    public int currentSubtitleIndex = -1;
    public bool displayingSubtitles;

    public void TriggerSubtitleFromIndex(int index)
    {
        currentSubtitleIndex = index;
        TriggerSubtitlesEvent.Raise();
    }
}
