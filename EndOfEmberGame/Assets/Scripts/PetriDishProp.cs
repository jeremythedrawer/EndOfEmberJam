using UnityEngine;

using static Biology;
public class PetriDishProp : MonoBehaviour
{
    public IconButton button;
    public AudioSource audioSource;

    public PetriDishSO petriDishData;
    public CameraSO camData;
    public InputSO inputData;
    public MicroscopeSO microscopeData;
    public ActionSO actionData;
    public AudioSO audioData;
    public PlayerSO playerData;

    [Header("Generated")]
    CellObject[] cellObjects;
    public Vector3 distFromCursor;
    public Vector3 worldPos;
    public Vector3 startPos;
    private void Start()
    {
        Init();
        CreateCells();
    }
    private void OnEnable()
    {
        actionData.onEnterMicroscopeView += StartCells;
    }
    private void OnDisable()
    {
        actionData.onEnterMicroscopeView -= StartCells;
    }
    private void Update()
    {
        button.UpdateButton();

        if (playerData.curView == ViewType.PetriDishTable && microscopeData.activePetriDishIndex == petriDishData.index)
        {
            worldPos.x = inputData.mouseWorldPos.x + distFromCursor.x;
            worldPos.y = inputData.mouseWorldPos.y + distFromCursor.y;

            transform.position = worldPos;
        }
    }
    private void Init()
    {
        void MouseDown()
        {
            button.MouseDown();
            microscopeData.activePetriDishIndex = petriDishData.index;
            distFromCursor.x = transform.position.x - inputData.mouseWorldPos.x;
            distFromCursor.y = transform.position.y - inputData.mouseWorldPos.y ;
        }
        void MouseUp()
        {
            button.MouseUp();

            float distFromMicroscope = Vector3.Distance(transform.position, microscopeData.triggerCircle.center);

            if (distFromMicroscope < microscopeData.triggerCircle.radius)
            {
                actionData.onToMicroscopeView?.Invoke();

                audioSource.PlayOneShot(audioData.petriDishDrop002);

                transform.position = microscopeData.triggerCircle.center;
            }
            else
            {
                audioSource.PlayOneShot(audioData.petriDishDrop001);
                microscopeData.activePetriDishIndex = -1;
            }
        }

        void MouseEnter()
        {
            button.MosueEnter();
            audioSource.PlayOneShot(audioData.petriDishHover001);
        }
        button.InitButton(onMouseUp: MouseUp, onMouseDown: MouseDown, onMouseEnter: MouseEnter);

        worldPos.z = transform.position.z;

        startPos = transform.position;
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
        if (microscopeData.activePetriDishIndex != petriDishData.index) return;

        for (int i = 0; i < CELL_COUNT;i++)
        {
            CellObject cellObject = cellObjects[i];

            cellObject.gameObject.SetActive(true);

            Vector3 cellPos = new Vector3();

            float xRandPos = Random.Range(-camData.bounds.extents.x, camData.bounds.extents.x);
            float yRandPos = Random.Range(-camData.bounds.extents.y, camData.bounds.extents.y);
            cellPos.x = camData.bounds.center.x + xRandPos;
            cellPos.y = camData.bounds.center.y + yRandPos;
            cellPos.z = 1;

            cellObject.transform.position = cellPos;

            float normID = (float)i / (float)CELL_COUNT;
            cellObject.SetPropBlock(normID);
        }
    }
}
