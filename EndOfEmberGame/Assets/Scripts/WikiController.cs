using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WikiController : MonoBehaviour
{
    public TMP_InputField inputField;
    public Image foundResultImage;

    public PlayerSO playerData;
    public WikiSO wikiData;

    public void Start()
    {
        foundResultImage.gameObject.SetActive(false);
    }

    public void HandleSearchFieldInput()
    {
        string keywordSearch = inputField.text.ToLowerInvariant();
        
        switch(keywordSearch)
        {
            case "chytrid":
            case "chytridiomycota":
            {
                foundResultImage.gameObject.SetActive(true);
                foundResultImage.sprite = wikiData.pageData[0].sprite;
                foundResultImage.SetNativeSize();
            }
            break;
            case "aspergillus":
            case "aspergillosis":
            {
                foundResultImage.gameObject.SetActive(true);
                foundResultImage.sprite = wikiData.pageData[1].sprite;
                foundResultImage.SetNativeSize();
            }
            break;
            case "conidia":
            case "spore":
            case "spores":
            {
                WikiSO.PageData pageData = wikiData.pageData[2];
                if(!pageData.hasPrinted)
                {
                    // page has not been printed yet print:
                    // play animation on photocopier
                    // play sfx
                    // show hoverable & clickable region on page coming out of printer
                    // then show full page on ui
                }
                else
                {
                    // just show page
                    foundResultImage.gameObject.SetActive(true);
                    foundResultImage.sprite = pageData.sprite;
                    foundResultImage.SetNativeSize();
                }
            }
            break;
            case "mitochondria":
            case "mitocondria":
            case "powerhouse":
            case "power":
            {
                foundResultImage.gameObject.SetActive(true);
                foundResultImage.sprite = wikiData.pageData[3].sprite;
                foundResultImage.SetNativeSize();
            }
            break;
        }


        // clear input field
        inputField.text = "";
        inputField.ActivateInputField();
    }
}
