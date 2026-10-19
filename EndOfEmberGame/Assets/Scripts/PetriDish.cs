using UnityEngine;

using static Biology;
public class PetriDish : MonoBehaviour
{
    public IconButton button;

    public PetriDishSO petriDishData;
    public CameraSO camData;

    [Header("Generated")]
    CellObject[] cellObjects;

    private void Start()
    {
        Init();
        CreateCells();
    }
    private void Update()
    {
        button.UpdateButton();
    }
    private void Init()
    {
        void MouseUp()
        {
            button.MouseUp();
            StartCells();
        }
        button.InitButton(onMouseUp: MouseUp);
    }
    private void CreateCells()
    {
        cellObjects = new CellObject[CELL_COUNT];
        for (int i = 0; i < CELL_COUNT; i++)
        {
            CellObject cell = Instantiate(petriDishData.cellObject, transform);
            cell.Init();
            cell.gameObject.SetActive(false);
            cellObjects[i] = cell;
        }
    }

    private void StartCells()
    {
        for (int i = 0; i < CELL_COUNT;i++)
        {
            CellObject cellObject = cellObjects[i];

            cellObject.gameObject.SetActive(true);

            Vector3 cellPos = new Vector3();

            cellPos.x = Random.Range(-camData.bounds.extents.x, camData.bounds.extents.x);
            cellPos.y = Random.Range(-camData.bounds.extents.y, camData.bounds.extents.y);
            cellPos.z = 1;

            cellObject.transform.localPosition = cellPos;

            float normID = (float)i / (float)CELL_COUNT;
            cellObject.SetPropBlock(normID);
        }
    }
}
