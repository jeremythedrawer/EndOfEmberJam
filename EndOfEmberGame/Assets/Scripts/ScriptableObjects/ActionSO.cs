using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionSO", menuName = "Custom / Action SO")]
public class ActionSO : ScriptableObject
{
    public Action onEnterWikiView;
    public Action onEnterRoomView;
    public Action onEnterMicroscopeView;
}
