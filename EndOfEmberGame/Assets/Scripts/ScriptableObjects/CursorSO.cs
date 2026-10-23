using UnityEngine;

[CreateAssetMenu(fileName = "CursorSO", menuName = "Custom / Cursor SO")]
public class CursorSO : ScriptableObject
{
    public Sprite spritePoint;
    public Sprite spriteHandDrag;
    public CursorSpriteType curCursorSpriteType = CursorSpriteType.Point;
}
