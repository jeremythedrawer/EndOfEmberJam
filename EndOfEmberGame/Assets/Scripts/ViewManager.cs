using System;
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


    // NOTE(Jazz): call this from a ui event to switch to a specific canvas.
    public void OpenDeskView()
    {
        foreach(var view in viewPrefabs)
        {
            view.SetActive(false);
            currentViewType = ViewType.Desk;
        }

        viewPrefabs[(int)currentViewType].SetActive(true);
    }

    public void OpenRoomView()
    {
        foreach(var view in viewPrefabs)
        {
            view.SetActive(false);
            currentViewType = ViewType.Room;
        }

        viewPrefabs[(int)currentViewType].SetActive(true);
    }
}
