using UnityEngine;

[CreateAssetMenu(fileName = "CameraSO", menuName = "Custom / Camera SO")]
public class CameraSO : ScriptableObject
{
    public Vector3 camWorldPos;
    public Vector3 wikiWorldPos;
    public Vector3 microscopeWorldPos;
    public Vector3 roomWorldPos;
}
