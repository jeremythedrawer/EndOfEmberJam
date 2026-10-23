using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CursorSO cursorData; 
    public void OnPointerEnter(PointerEventData eventData)
    {
        // change cursor data
        cursorData.curCursorSpriteType = CursorSpriteType.HandDrag;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // change cursor data
        cursorData.curCursorSpriteType = CursorSpriteType.Point;
    }
}
