using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Custom / Player SO")]
public class PlayerSO : ScriptableObject
{
    public enum State
    {
        None,
        Room,
        Microscope,
        Wiki,
    }

    public State curState;
}
