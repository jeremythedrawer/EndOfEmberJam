using Proselyte.Sigils;
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
    public SubtitlesSO subtitleData;
    public GameEvent powerOutageEvent;
    public Button gotoButtonLab;
    public Button gotoButtonDesk;

    public GameEvent OpenViewLabEvent;
    public GameEvent OpenViewDeskEvent;

    public GameObject thermostatBackgroundGO;
    public Button thermostatWallButton;
    public GameEvent SetCoolModeEvent;

    public enum ViewType
    {
        Room,           // 0
        Desk,           // 1
        Lab,            // 2
        Microscope,     // 3
        Attic,          // 4
        MAX_COUNT
    } public ViewType currentViewType;

    private void OnEnable()
    {
        gotoButtonLab.onClick.AddListener(OpenViewLabEvent.Raise);
        gotoButtonDesk.onClick.AddListener(OpenViewDeskEvent.Raise);
        powerOutageEvent.RegisterListener(OnPowerOutageEvent);
        thermostatWallButton.onClick.AddListener(() => thermostatBackgroundGO.SetActive(true));
    }

    private void OnDisable()
    {
        gotoButtonLab.onClick.RemoveListener(OpenViewLabEvent.Raise);
        gotoButtonDesk.onClick.RemoveListener(OpenViewDeskEvent.Raise);
        powerOutageEvent.UnregisterListener(OnPowerOutageEvent);
    }

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

    private void OnPowerOutageEvent()
    {
        // remove all event callbacks from desk, lab, thermostat
        gotoButtonDesk.onClick.RemoveAllListeners();
        gotoButtonLab.onClick.RemoveAllListeners();
        thermostatWallButton.onClick.RemoveAllListeners();

        // apply subtitle callbacks
        gotoButtonDesk.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(21));
        gotoButtonLab.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(21));
        thermostatWallButton.onClick.AddListener(() => subtitleData.TriggerSubtitleFromIndex(21));

        // turn heating off
        SetCoolModeEvent.Raise();
    }
}
