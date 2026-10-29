using UnityEngine;

public class PowerOutageController : MonoBehaviour
{
    public Material powerOutageMaterial;
    public PlayerSO playerData;
    public InputSO inputData;
    private Vector3 mousePosParam;

    private readonly int mousePosID = Shader.PropertyToID("_MousePos");

    private void Update()
    {
        mousePosParam = inputData.mouseScreenPos;
        powerOutageMaterial.SetVector(mousePosID, mousePosParam);
    }
}
