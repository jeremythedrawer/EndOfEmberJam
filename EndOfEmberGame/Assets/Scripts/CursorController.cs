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
    Inspect = 5,
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

    private float ctr = 0f;
    public float cursorAnimTime = 0.8f;
    private int cursorAnimIndex = 0;
    private int cursorAnimFrameMax = 3;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        cursorShowEvent.RegisterListener(OnCursorShow);
        cursorHideEvent.RegisterListener(OnCursorHide);
        SettingSO.onApplySetting += UpdateCursorSize;
    }

    private void OnDisable()
    {
        cursorShowEvent.UnregisterListener(OnCursorShow);
        cursorHideEvent.UnregisterListener(OnCursorHide);
        SettingSO.onApplySetting -= UpdateCursorSize;
    }

    private void Update()
    {
        if(ctr > cursorAnimTime)
        {
            ctr = 0;
            cursorAnimIndex++;
            if(cursorAnimIndex > cursorAnimFrameMax - 1) cursorAnimIndex = 0;
        }
        ctr += Time.deltaTime;

        // set cursor position
        cursorTransform.position = inputData.mouseScreenPos;

        switch (cursorData.curCursorSpriteType)
        {
            case CursorSpriteType.Point:
            {
                cursorImage.sprite = cursorData.spritePoint[cursorAnimIndex];
            }
            break;
            case CursorSpriteType.ArrowUp:
            {
                cursorImage.sprite = cursorData.spriteArrowUp;
            }
            break;
            case CursorSpriteType.ArrowDown:
            {
                cursorImage.sprite = cursorData.spriteArrowDown;
            }
            break;
            case CursorSpriteType.HandOpen:
            {
                cursorImage.sprite = cursorData.spriteHandOpen;
            }
            break;
            case CursorSpriteType.HandDrag:
            {
                cursorImage.sprite = cursorData.spriteHandDrag[cursorAnimIndex];
            }
            break;
            case CursorSpriteType.Inspect:
            {
                cursorImage.sprite = cursorData.spriteInspect;
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

    void UpdateCursorSize()
    {
        cursorTransform.localScale = Vector3.one * SettingSO.cursorSize * 0.025f;
    }
}
