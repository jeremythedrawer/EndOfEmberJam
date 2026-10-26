using UnityEngine;
using UnityEngine.UI;

public class PetriDishSnap : MonoBehaviour
{
    [SerializeField] MicroscopeSO microscopeSO; //for debug purpose. idealy set this variable in the microscope
    public static PetriDishSnap instance;

    void Awake()
    {
        instance = this;
    }

    public void DetectDish(PetriDishDragable dish)
    {
        //snap if current dish is within range
        Vector2 dishPosition = dish.transform.localPosition;
        float distance = Vector2.Distance(dish.transform.localPosition, transform.localPosition);
        bool withinRange = (distance <= 150) ? true : false;
        if (microscopeSO.curCellType == 0)
        {
            if (withinRange)
            {
                dish.transform.position = transform.position;
                microscopeSO.curCellType = dish.petriDishData.cellType;
            }
        }
        // remove the current dish
        else if (microscopeSO.curCellType == dish.petriDishData.cellType)
        {
            if (!withinRange)
            {
                microscopeSO.curCellType = Biology.CellType.None;
            }
        }
    }
}
