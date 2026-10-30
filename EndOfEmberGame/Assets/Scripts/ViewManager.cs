using UnityEngine;
using UnityEngine.UI;

// NOTE(Jazz): Controls - What view are we in? Where do we go from here?
public class ViewManager : MonoBehaviour
{
    public GameObject[] viewPrefabs;
    public GraphicRaycaster[] viewGraphicRaycasters;
    public GraphicRaycaster hudGraphicRaycaster;
    public GraphicRaycaster introGraphicRaycaster;

    public PlayerSO playerData;

    public enum ViewType
    {
        Room,           // 0
        Desk,           // 1
        Lab,            // 2
        Microscope,     // 3
        Attic,          // 4
        MAX_COUNT
    } public ViewType currentViewType;

    private void Start()
    {
        currentViewType = ViewType.Room;
        DisableAllViews();
        EnableView(currentViewType);
    }

    public void OpenViewRoom()       { DisableAllViews(); EnableView(ViewType.Room); }
    public void OpenViewDesk()       { DisableAllViews(); EnableView(ViewType.Desk); }
    public void OpenViewLab()        { DisableAllViews(); EnableView(ViewType.Lab); }
    public void OpenViewMicroscope() { DisableAllViews(); EnableView(ViewType.Microscope); }
    public void OpenViewAttic()      { DisableAllViews(); EnableView(ViewType.Attic); }

    public void OnInteractionEnable()
    {
        foreach(var gRaycaster in viewGraphicRaycasters)
        {
            gRaycaster.enabled = true;
        }
        hudGraphicRaycaster.enabled = false;
    }

    public void OnInteractionDisable()
    {
        foreach(var gRaycaster in viewGraphicRaycasters)
        {
            gRaycaster.enabled = false;
        }
        hudGraphicRaycaster.enabled = true;
    }

    public void OnIntroEnable()
    {
        if(!playerData.introCompleted)
        {
            introGraphicRaycaster.gameObject.SetActive(true);
            introGraphicRaycaster.enabled = true;
        }
    }

    public void OnIntroDisable()
    {
        playerData.introCompleted = true;
        introGraphicRaycaster.enabled = false;
        introGraphicRaycaster.gameObject.SetActive(false);
    }

    private void EnableView(ViewType view)
    {
        viewPrefabs[(int)view].SetActive(true);
    }

    private void DisableAllViews()
    {
        foreach(var view in viewPrefabs) view.SetActive(false);
    }
}
