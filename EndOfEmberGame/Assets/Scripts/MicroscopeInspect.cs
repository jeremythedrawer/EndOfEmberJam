using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Biology;

public class MicroscopeInspect : MonoBehaviour, IPointerEnterHandler
{
    public MicroscopeSO microData;
    public PlayerSO playerData;
    public SubtitlesSO subtitleData;

    public Button inspectButton;

    public void OnPointerEnter(PointerEventData eventData)
    {
        CellType currentCellType = MicroscopeSO.curCellType;

        inspectButton.onClick.RemoveAllListeners();

        switch (currentCellType)
        {
            case CellType.None:
            {
                if(playerData.placedPetriDishOnMicroscopeAtLeastOnce)
                {
                    inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(2));
                }
                else
                {
                    inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(6));
                }
            }
            break;
            case CellType.Fungus:
            {

                inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(4));
            }
            break;
            case CellType.Mammal:
            {
                inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(5));
            }
            break;
            case CellType.Plant:
            {
                inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(3));
            }
            break;
        }
    }
}
