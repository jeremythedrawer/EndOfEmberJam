using Proselyte.Sigils;
using UnityEngine;
using UnityEngine.UI;

public enum CursorSpriteType : byte
{
    Point = 0,
    ArrowUp = 1,
    ArrowDown = 2,
    HandOpen = 3,
    HandDrag = 4,
    MAX_COUNT
}

public class CursorController : MonoBehaviour
{
    public InputSO inputData;
    public CursorSO cursorData;
    public Transform cursorTransform;
    public Image cursorImage;

    public GameEvent cursorHideEvent;
    public GameEvent cursorShowEvent;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        cursorShowEvent.RegisterListener(OnCursorShow);
        cursorHideEvent.RegisterListener(OnCursorHide);
    }

    private void OnDisable()
    {
        cursorShowEvent.UnregisterListener(OnCursorShow);
        cursorHideEvent.UnregisterListener(OnCursorHide);
    }

    private void Update()
    {
        // set cursor position
        cursorTransform.position = inputData.mouseScreenPos;

        switch (cursorData.curCursorSpriteType)
        {
            case CursorSpriteType.Point:
            {
                cursorImage.sprite = cursorData.spritePoint;
            }
            break;
            case CursorSpriteType.HandDrag:
            {
                cursorImage.sprite = cursorData.spriteHandDrag;
            }
            break;
        }
    }

    public void OnCursorHide()
    {
        cursorImage.enabled = false;
    }

    public void OnCursorShow()
    {
        cursorImage.enabled = true;
    }
}
