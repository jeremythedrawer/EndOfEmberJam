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
                if(!playerData.inspectedCellsFungus)
                {
                    // initial inspection of fungus
                    inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(4));
                    playerData.inspectedCellsFungus = true;
                }
                else if(playerData.currentRoomTemperature > playerData.minFungalHeatTemperature)
                {
                    if(!playerData.inspectedCellsFungusHeated && !playerData.researchedThermophilic)
                    {
                        inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(7));
                        playerData.inspectedCellsFungusHeated = true;
                    }
                    else if(playerData.inspectedCellsFungusHeated && !playerData.researchedThermophilic)
                    {
                        inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(11));
                    }
                    else
                    {
                        inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(8));
                    }
                }
                else
                {
                    if(!playerData.inspectedCellsFungusHeated && !playerData.researchedThermophilic)
                    {
                        inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(9));
                    }
                    if(!playerData.inspectedCellsFungusHeated && playerData.researchedThermophilic)
                    {
                        inspectButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(10));
                    }

                }
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
