using UnityEngine;

// NOTE(Jazz): Controls - What view are we in? Where do we go from here?
public class ViewManager : MonoBehaviour
{
    public GameObject[] viewPrefabs;

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

    private void EnableView(ViewType view)
    {
        currentViewType = ViewType.Attic;
        viewPrefabs[(int)view].SetActive(true);
    }

    private void DisableAllViews()
    {
        foreach(var view in viewPrefabs) view.SetActive(false);
    }

    public void OpenRoomView()
    {
        DisableAllViews();
        currentViewType = ViewType.Room;
        viewPrefabs[(int)currentViewType].SetActive(true);
    }
}
