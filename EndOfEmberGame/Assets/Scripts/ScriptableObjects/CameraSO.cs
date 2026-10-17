using UnityEngine;

[CreateAssetMenu(fileName = "CameraSO", menuName = "Custom / Camera SO")]
public class CameraSO : ScriptableObject
{
    public Material postProcessMaterial;
    public float damping;
    public float fadeTime;
    [Header("Generated")]
    public Bounds bounds;

    public Vector3 curCamWorldPos;
    public Vector3 preCamWorldPos;
    public Vector3 wikiWorldPos;
    public Vector3 microscopeWorldPos;
    public Vector3 roomWorldPos;

    public float worldUnitsPerPixel;
}
