using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerMoveHandler
{
    public CursorSO cursorData;

    public Material imagemat;
    private readonly int hoveringID = Shader.PropertyToID("_hovering");
    private readonly int timeID = Shader.PropertyToID("_time");
    private float time;
    private bool hovering;
    public bool persistentButton;

    public CursorSpriteType cursorHoverType;

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorData.curCursorSpriteType = cursorHoverType;

        if(imagemat == null) return;
        hovering = true;
        imagemat.SetFloat(hoveringID, 1f);
        imagemat.SetFloat(timeID, time);
    }

    public void ApplyCursorType()
    {
        cursorData.curCursorSpriteType = cursorHoverType;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnHoverButton();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        cursorData.curCursorSpriteType = cursorHoverType;
        if(imagemat == null) return;
        hovering = true;
        imagemat.SetFloat(hoveringID, 1f);
        imagemat.SetFloat(timeID, time);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!persistentButton)
        {
            UnHoverButton();
        }
    }

    private void UnHoverButton()
    {
        // change cursor to default and reset material
        cursorData.curCursorSpriteType = CursorSpriteType.Point; //default

        if(imagemat == null) return;
        hovering = false;
        imagemat.SetFloat(hoveringID, 0f);
        imagemat.SetFloat(timeID, 0f);
    }

    private void Update()
    {
        // Hovering flashing material effect
        if(hovering)
        {
            time += Time.deltaTime;
            if(imagemat == null) return;
            imagemat.SetFloat(timeID, time);
        }
        else
        {
            time = 0;
        }
    }
}


