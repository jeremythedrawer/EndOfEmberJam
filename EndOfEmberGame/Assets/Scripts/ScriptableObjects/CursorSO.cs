using UnityEngine;

[CreateAssetMenu(fileName = "CursorSO", menuName = "Custom / Cursor SO")]
public class CursorSO : ScriptableObject
{
    public Sprite[] spritePoint;
    public Sprite[] spriteHandDrag;
    public Sprite spriteHandOpen;
    public Sprite spriteArrowUp;
    public Sprite spriteArrowDown;
    public Sprite spriteInspect;
    public CursorSpriteType curCursorSpriteType = CursorSpriteType.Point;
}
