using Proselyte.Sigils;
using UnityEngine;
using UnityEngine.UI;

public class PowerOutageController : MonoBehaviour
{
    public Material powerOutageMaterial;
    public Image overlayImage;
    public PlayerSO playerData;
    public InputSO inputData;

    public GameEvent powerOutageEvent;

    private Vector3 mousePosParam;
    private readonly int mousePosID = Shader.PropertyToID("_MousePos");

    private void OnEnable()
    {
        powerOutageEvent.RegisterListener(TriggerPowerOutageOverlay);
    }

    private void OnDisable()
    {
        powerOutageEvent.UnregisterListener(TriggerPowerOutageOverlay);
    }

    private void TriggerPowerOutageOverlay()
    {
        overlayImage.color = new Color(0, 0, 0, 0.9f);
    }

    private void Update()
    {
        mousePosParam = inputData.mouseScreenPos;
        powerOutageMaterial.SetVector(mousePosID, mousePosParam);
    }
}
