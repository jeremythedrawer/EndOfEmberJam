using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public CursorSO cursorData;

    public Material imagemat;
    private readonly int hoveringID = Shader.PropertyToID("_hovering");
    private readonly int timeID = Shader.PropertyToID("_time");
    private float time;
    private bool hovering;
    public bool persistentButton;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // change cursor data
        cursorData.curCursorSpriteType = CursorSpriteType.HandDrag;

        if(imagemat == null) return;
        hovering = true;
        imagemat.SetFloat(hoveringID, 1f);
        imagemat.SetFloat(timeID, time);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnHoverButton();
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
        if(hovering)
        {
            time += Time.deltaTime;
            imagemat.SetFloat(timeID, time);
        }
        else
        {
            time = 0;
        }
    }
}


