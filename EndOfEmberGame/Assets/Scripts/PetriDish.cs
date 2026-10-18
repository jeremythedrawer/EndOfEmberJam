using UnityEngine;

using static Biology;
public class PetriDish : MonoBehaviour
{
    public IconButton button;

    public PetriDishSO petriDishData;

    [Header("Generated")]
    CellObject[] cellObjects;

    private void Start()
    {
        CreateCells();
    }
    private void CreateCells()
    {
        cellObjects = new CellObject[CELL_COUNT];
        for (int i = 0; i < CELL_COUNT; i++)
        {
            CellObject cell = Instantiate(petriDishData.cellObject);
            cell.gameObject.SetActive(false);
            cellObjects[i] = cell;
        }
    }

    private void StartCells()
    {
        for (int i = 0; i < CELL_COUNT;i++)
        {
            CellObject cellObject = cellObjects[i];

        }
    }
}
