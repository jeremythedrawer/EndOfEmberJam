using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Biology;

public class MicroscopeController : MonoBehaviour
{
    [SerializeField] MicroscopeSO microscopeData;
    [SerializeField] MicroscopeViewData[] spriteDatas;
    [SerializeField] Image viewSprite;
    [SerializeField] float defaultFrameSpeed = 0.2f;
    private MicroscopeViewData currentData;

#if UNITY_EDITOR
    private void Awake()
    {
        MicroscopeSO.curCellType = CellType.None;
    }
#endif

    void OnEnable()
    {
        UpdateSprite();
        if (currentData != null)
        {
            StartCoroutine(playAnination());
        }
        else
        {
            viewSprite.sprite = null;
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    public void UpdateSprite()
    {
        currentData = Array.Find(spriteDatas, MatchType);
    }

    private bool MatchType(MicroscopeViewData data)
    {
        return data.type == MicroscopeSO.curCellType;
    }

    private IEnumerator playAnination()
    {
        WaitForSeconds wait = new WaitForSeconds(defaultFrameSpeed);
        int index = 0;
        while (true)
        {
            viewSprite.sprite = currentData.sprites[index];
            index++;
            if (index >= currentData.sprites.Length)
            {
                index = 0;
            }
            yield return wait;
        }
    }
}

[Serializable]
public class MicroscopeViewData
{
    public CellType type;
    public Sprite[] sprites;
}
