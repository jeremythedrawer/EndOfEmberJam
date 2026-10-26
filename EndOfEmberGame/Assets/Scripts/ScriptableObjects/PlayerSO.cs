using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Custom / Player SO")]
public class PlayerSO : ScriptableObject
{
    public bool windowOpen;
    public bool powerboxOpen;
    public bool powerboxFixed;
    public bool fanMode;
    public int currentRoomTemperature;
}
