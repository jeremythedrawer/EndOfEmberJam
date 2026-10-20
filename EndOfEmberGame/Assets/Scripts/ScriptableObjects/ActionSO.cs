using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionSO", menuName = "Custom / Action SO")]
public class ActionSO : ScriptableObject
{
    public Action onToPetriDishTableView;
    public Action onToRoomView;
    public Action onToMicroscopeView;

    public Action onEnterPetriDishView;
    public Action onEnterRoomView;
    public Action onEnterMicroscopeView;

    public Action onExitMicroscopeView;
}
