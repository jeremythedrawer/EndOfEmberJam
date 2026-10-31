using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Proselyte.Sigils;
using UnityEngine.EventSystems;

public class WikiController : MonoBehaviour
{
    public TMP_InputField inputField;
    public Image foundResultImage;
    public GameObject foundResultBackdropGO;

    public PlayerSO playerData;
    public WikiSO wikiData;
    public SubtitlesSO subtitlesData;

    public GameEvent confirmWikiPageEvent;
    public GameEvent subtitleConfirmEvent;
    public GameEvent powerRestoredEvent;
    public GameEvent wikiPageSuccessEvent;

    private void OnEnable()
    {
        confirmWikiPageEvent.RegisterListener(OnConfirmWikiPage);
    }

    private void OnDisable()
    {
        confirmWikiPageEvent.UnregisterListener(OnConfirmWikiPage);
    }

    public void Start()
    {
        foundResultBackdropGO.SetActive(false);
    }



    public void HandleSearchFieldInput()
    {
        string keywordSearch = inputField.text.ToLowerInvariant();
        
        switch(keywordSearch)
        {
            case "chytrid":
            case "chytridiomycota":
            {
                ShowPage(0);
            }
            break;
            case "aspergillus":
            case "aspergillosis":
            case "mould":
            case "mold":
            {
                ShowPage(1);
            }
            break;
            case "conidia":
            case "conidium":
            case "spore":
            case "spores":
            {
                ShowPage(2);
            }
            break;
            case "cell":
            case "mitochondria":
            case "mitocondria":
            case "powerhouse":
            case "power":
            {
                playerData.researchedMitochondria = true;
                ShowPage(3);
                foundResultImage.rectTransform.sizeDelta = foundResultImage.rectTransform.sizeDelta * 0.5f;
            }
            break;
            case "parasite":
            case "parasitism":
            case "parasitoid":
            {
                ShowPage(4);
            }
            break;
            case "thermophilic":
            case "thermal":
            case "heat":
            {
                playerData.researchedThermophilic = true;
                ShowPage(5);
            }
            break;
            case "dormant": 
            case "dormancy": 
            case "latency":
            {
                ShowPage(6);
            }
            break;
            case "phototactic": 
            case "phototaxis":
            case "light sensitive":
            case "photophobic":
            case "motile":
            {
                ShowPage(7);
                powerRestoredEvent.Raise();
            }
            break;
            case "hyphae":
            case "mycelium":
            case "hyphal":
            case "filamentous":
            case "mycostructures":
            case "mycostructure":
            case "permeable":
            case "enzyme":
            case "enzymes":
            case "growth":
            case "epithelial":
            case "epithelial tissue":
            case "epithelial tissues":
            case "tissue":
            {
                ShowPage(8);
            }
            break;
            case "zoophthora":
            {
                ShowPage(9);
            }
            break;
            // TODO(Jazz): create saprotroph page
            //case "saprobe":
            //case "saprotrophic":
            //case "saprotroph":
            //{
            //    ShowPage(6);
            //}
            //break;
            case "":
            {
                // leave blank no input detected
            }
            break;
            default: // handle invalid input
            {
                subtitlesData.TriggerSubtitleFromIndex(0);
                DeactivateInputField();
            }
            break;
        }
    }

    public void ShowPage(int pageIndex)
    {
        foundResultBackdropGO.SetActive(true);
        foundResultImage.sprite = wikiData.pageData[pageIndex].sprite;
        foundResultImage.SetNativeSize();
        wikiData.pageIsShowing = true;
        wikiPageSuccessEvent.Raise();

        // clear input field

        DeactivateInputField();
    }

    private void OnConfirmWikiPage()
    {
        if(wikiData.pageIsShowing)
        {
            subtitleConfirmEvent.Raise();
            foundResultBackdropGO.SetActive(false);
            inputField.ActivateInputField();
            inputField.text = string.Empty;
            wikiData.pageIsShowing = false;
        }
        else if (subtitlesData.displayingSubtitles)
        {
            // player confirmed subtitles while using the wiki, refocus the input field for the wiki
            inputField.ActivateInputField();
            inputField.text = string.Empty;
        }
    }

    public void OnSelectInputField()
    {
        inputField.text = string.Empty;
    }

    private void DeactivateInputField()
    {
        inputField.DeactivateInputField();
        var eventSystem = EventSystem.current;
        if(!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
    }
}
