using System.Collections;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetriDishDragable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] InputSO inputSO;
    [SerializeField] CursorSO cursorData;
    public PetriDishSO petriDishData;
    private Coroutine trackingFunction;
    private Coroutine returnFunction;
    private Vector3 origin;

    void Awake()
    {
        origin = transform.localPosition;
    }

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
                transform.SetAsLastSibling();
                spriteOffset = transform.position - inputSO.mouseWorldPos;
                if (returnFunction != null)
                {
                    StopCoroutine(returnFunction);
                }
            }
            if (inputSO.mouseLeftHold && spriteOffset != Vector3.zero)
            {
                transform.position = new Vector3(spriteOffset.x + inputSO.mouseWorldPos.x, spriteOffset.y + inputSO.mouseWorldPos.y, transform.position.z);
            }
            if (inputSO.mouseLeftUp)
            {
                spriteOffset = Vector3.zero;
                if (!PetriDishSnap.instance.DetectDish(this))
                {
                    returnFunction = StartCoroutine(ReturnToOrigin());
                }
            }
            yield return null;
        }
    }

    private IEnumerator ReturnToOrigin()
    {
        while (true)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, origin, 1000f*Time.deltaTime);
            if (transform.localPosition == origin)
            {
                break;
            }
            yield return null;
        }
    }
}
