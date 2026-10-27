using System.Collections;
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

    public AudioSource petriDishAudioSource;
    public AudioClip clipHover;
    public AudioClip clipDropTable;
    public AudioClip clipDropMicroscope;
    public OnHover onHover;

    void Awake()
    {
        origin = transform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        trackingFunction = StartCoroutine(TrackDraggingInput());
        petriDishAudioSource.PlayOneShot(clipHover);
        onHover.cursorHoverType = CursorSpriteType.HandOpen;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(trackingFunction);
        onHover.cursorHoverType = CursorSpriteType.HandOpen;
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
                onHover.cursorHoverType = CursorSpriteType.HandDrag;
                onHover.ApplyCursorType();
            }
            if (inputSO.mouseLeftHold && spriteOffset != Vector3.zero)
            {
                transform.position = new Vector3(spriteOffset.x + inputSO.mouseWorldPos.x, spriteOffset.y + inputSO.mouseWorldPos.y, transform.position.z);
                onHover.cursorHoverType = CursorSpriteType.HandDrag;
            }
            if (inputSO.mouseLeftUp)
            {
                spriteOffset = Vector3.zero;
                if (!PetriDishSnap.instance.DetectDish(this))
                {
                    returnFunction = StartCoroutine(ReturnToOrigin());
                }
                else
                {
                    petriDishAudioSource.PlayOneShot(clipDropMicroscope);
                }
                onHover.cursorHoverType = CursorSpriteType.HandOpen;
                onHover.ApplyCursorType();
            }
            yield return null;
        }
    }

    private IEnumerator ReturnToOrigin()
    {
        while (true)
        {
            // NOTE(Jazz): snap on return instead
            transform.localPosition = origin;
            petriDishAudioSource.PlayOneShot(clipDropTable);
            break;

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, origin, 1000f*Time.deltaTime);
            if (transform.localPosition == origin)
            {
                break;
            }
            yield return null;
        }
    }
}
