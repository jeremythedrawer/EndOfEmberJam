using UnityEngine;

using static Biology;

[CreateAssetMenu(fileName = "MicroscopeSO", menuName = "Custom / Microscope SO")]
public class MicroscopeSO : ScriptableObject
{
    public PetriDishSO[] petriDishDatas;
    public TriggerCircle triggerCircle;
    public CellType curCellType;
    public int activePetriDishIndex;
}
