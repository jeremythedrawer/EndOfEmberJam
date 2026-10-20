using UnityEngine;
using static Biology;

[CreateAssetMenu(fileName = "PetriDishSO", menuName = "Custom / Petri Dish SO")]
public class PetriDishSO : ScriptableObject
{
    public CellType cellType;
    public CellObject cellObject;

    [Header("Generated")]
    public Vector2[] positions;
    public int index;
}
