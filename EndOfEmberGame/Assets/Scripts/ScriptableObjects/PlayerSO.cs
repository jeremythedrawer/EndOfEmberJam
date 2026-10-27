using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Custom / Player SO")]
public class PlayerSO : ScriptableObject
{
    public int currentRoomTemperature;
    public bool windowOpen;
    public bool powerboxOpen;
    public bool powerboxFixed;
    public bool fanMode;
    
    public bool inspectedCellsPlant;
    public bool inspectedCellsFungus;
    public bool inspectedCellsFungusHeated;
    public bool inspectedCellsMammal;

    public bool placedPetriDishOnMicroscopeAtLeastOnce;
}
