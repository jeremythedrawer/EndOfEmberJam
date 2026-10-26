using UnityEngine;

[CreateAssetMenu(fileName = "WikiSO", menuName = "ScriptableObjects / WikiSO")]
public class WikiSO : ScriptableObject
{
    [System.Serializable]
    public struct PageData
    {
        public bool hasPrinted;

        public Sprite sprite;
    }
    public PageData[] pageData;
    public bool pageIsShowing;
}
