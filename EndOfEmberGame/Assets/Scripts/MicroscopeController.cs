using System;
using UnityEngine;
using static Biology;

public class MicroscopeController : MonoBehaviour
{
    [SerializeField] MicroscopeSO microscopeData;
    [SerializeField] MicroscopeViewData[] spriteDatas;
    private MicroscopeViewData currentData;

    public void UpdateSprite()
    {
        currentData = Array.Find(spriteDatas, MatchType);
        if (currentData == null)
        {
            return;
        }
    }

    private bool MatchType(MicroscopeViewData data)
    {
        return data.type == microscopeData.curCellType;
    }


}

[Serializable]
public class MicroscopeViewData
{
    public CellType type;
    public Sprite[] sprites;
}
