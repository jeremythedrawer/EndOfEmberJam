using Proselyte.Sigils;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerOutageController : MonoBehaviour
{
    public Material powerOutageMaterial;
    public Image overlayImage;
    public PlayerSO playerData;
    public InputSO inputData;

    public GameEvent powerOutageEvent;
    public GameEvent powerRestoredEvent;
    public GameEvent flickerLightsEvent;

    private Vector3 mousePosParam;
    private readonly int mousePosID = Shader.PropertyToID("_MousePos");
    private Coroutine flickerCoroutine;
    private Color lightColorOff = new Color(0,0,0,0.9f);
    private Color lightColorOn = new Color(0,0,0,0f);

    private void OnEnable()
    {
        powerOutageEvent.RegisterListener(TriggerPowerOutageOverlay);
        powerRestoredEvent.RegisterListener(TriggerLightFlicker);
        flickerLightsEvent.RegisterListener(TriggerLightFlicker);
    }

    private void OnDisable()
    {
        powerOutageEvent.UnregisterListener(TriggerPowerOutageOverlay);
        powerRestoredEvent.UnregisterListener(TriggerLightFlicker);
        flickerLightsEvent.UnregisterListener(TriggerLightFlicker);
        powerOutageMaterial.SetVector(mousePosID, new Vector4());
    }

    private void TriggerPowerOutageOverlay()
    {
        overlayImage.color = new Color(0, 0, 0, 0.9f);
    }

    private void TriggerLightFlicker()
    {
        if(flickerCoroutine != null) StopCoroutine(flickerCoroutine);
        flickerCoroutine = StartCoroutine(LightFlicker());
    }

    private void Update()
    {
        mousePosParam = inputData.mouseScreenPos;
        powerOutageMaterial.SetVector(mousePosID, mousePosParam);
    }

    private IEnumerator LightFlicker()
    {
        float ctr = 0;
        float flickerMaxTime = 0.3f;
        float ctrMax = flickerMaxTime * 2f;
        float flickerCtr = 0;
        float flickerLingerTimeMax = 0.05f;
        float flickerLingerTimeCurrent = 0.00f;

        while(ctr < ctrMax)
        {
            if(flickerCtr > flickerMaxTime)
            {
                flickerLingerTimeCurrent = 0;
                flickerCtr = 0;
            }
            else
            {
                flickerCtr += Time.deltaTime;
                flickerLingerTimeCurrent += Time.deltaTime;
                if(flickerLingerTimeCurrent > flickerLingerTimeMax)
                {
                    overlayImage.color = lightColorOff;
                }
                else
                {
                    overlayImage.color = lightColorOn;
                }
            }
            
            ctr += Time.deltaTime;
            yield return null;
        }

        overlayImage.color = lightColorOn;
        playerData.powerOut = false;
    }
}
