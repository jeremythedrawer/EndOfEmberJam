using Proselyte.Sigils;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Custom / Player SO")]
public class PlayerSO : ScriptableObject
{
    public int currentRoomTemperature;
    public int minFungalHeatTemperature = 25;
    public int moldStageCurrent;

    public bool introCompleted;
    public bool windowOpen;
    public bool windowOpenedAtLeastOnce;
    public bool powerboxOpen;
    public bool powerboxFixed;
    public bool powerOut;
    public bool fanMode;


    public bool placedPetriDishOnMicroscopeAtLeastOnce;
    
    public bool inspectedCellsPlant;
    public bool inspectedCellsFungus;
    public bool inspectedCellsFungusHeated;
    public bool inspectedCellsMammal;

    public bool microscopeCompleted;

    public bool researchedThermophilic;
    public bool researchedMitochondria;
    public bool researchedPhototactic;

    public bool discoveredSpecimenIsMold;
    public bool discoveredSpecimenIsAerobic;
    public bool discoveredSpecimenIsThermophilic;
    public bool discoveredSpecimenIsPhototactic;

    public GameEvent moldStageIncreaseEvent;

    public void IncreaseMoldStage()
    {
        moldStageCurrent += 1;
        if(moldStageCurrent > 7) moldStageCurrent = 7;
        moldStageIncreaseEvent.Raise();
    }
}
