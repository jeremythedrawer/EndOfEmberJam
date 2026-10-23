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

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Start()
    {
        
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
}
