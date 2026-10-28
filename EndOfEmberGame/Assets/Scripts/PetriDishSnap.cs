using UnityEngine;
using UnityEngine.UI;

public class PetriDishSnap : MonoBehaviour
{
    public static PetriDishSnap instance;
    [SerializeField] GameObject overlayObject;

    void Awake()
    {
        instance = this;
    }

    public bool DetectDish(PetriDishDragable dish)
    {
        //snap if current dish is within range
        Vector2 dishPosition = dish.transform.localPosition;
        float distance = Vector2.Distance(dish.transform.localPosition, transform.localPosition);
        bool withinRange = (distance <= 150) ? true : false;
        /*if (MicroscopeSO.curCellType == 0)
        {
            if (withinRange)
            {
                SnapDish(dish);
                return true;
            }
        }
        else if (MicroscopeSO.curCellType == dish.petriDishData.cellType)
        {
            if (!withinRange)
            {
                MicroscopeSO.curCellType = Biology.CellType.None;
            }
            else
            {
                overlayObject.transform.SetAsLastSibling();
            }

        }*/
        if (withinRange)
        {
            if (MicroscopeSO.curCellType == Biology.CellType.None || MicroscopeSO.curCellType == dish.petriDishData.cellType)
            {
                SnapDish(dish);
                return true;
            }
        }
        else if (MicroscopeSO.curCellType == dish.petriDishData.cellType)
        {
            MicroscopeSO.curCellType = Biology.CellType.None;

        }
        return false;
    }

    private void SnapDish(PetriDishDragable dish)
    {
        dish.transform.position = transform.position;
        overlayObject.transform.SetAsLastSibling();
        MicroscopeSO.curCellType = dish.petriDishData.cellType;
    }
}
