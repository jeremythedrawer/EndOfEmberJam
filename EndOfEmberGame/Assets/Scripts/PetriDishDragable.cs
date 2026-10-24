using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetriDishDragable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] InputSO inputSO;
    [SerializeField] CursorSO cursorData;
    private Coroutine trackingFunction;

    public void OnPointerEnter(PointerEventData eventData)
    {
        trackingFunction = StartCoroutine(TrackDraggingInput());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(trackingFunction);
    }

    private IEnumerator TrackDraggingInput()
    {
        Vector3 spriteOffset = Vector3.zero;
        while (true)
        {
            if (inputSO.mouseLeftDown)
            {
                spriteOffset = transform.position - inputSO.mouseWorldPos;
            }
            if (inputSO.mouseLeftHold && spriteOffset != Vector3.zero)
            {
                transform.position = new Vector3(spriteOffset.x + inputSO.mouseWorldPos.x, spriteOffset.y + inputSO.mouseWorldPos.y, transform.position.z);
            }
            yield return null;
        }
    }
}
