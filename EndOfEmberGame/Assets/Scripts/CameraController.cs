using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

using static Atlas;
public class CameraController : MonoBehaviour
{
    public delegate void OnFinishFadeIn();

    public CameraSO camData;
    public ActionSO actionData;
    public PlayerSO playerData;
    public OptionsSO options;
    [Header("Generated")]
    
    public Camera cam;

    public Vector3 rawCurWorldPos;
    public Vector3 targetWorldPos;

    public float fadeClock;

    public CancellationTokenSource ctsFade;
    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        actionData.onEnterMicroscopeView += EnterMicroscopeView;
        actionData.onEnterRoomView += EnterRoomView;
        actionData.onEnterWikiView += EnterWikiView;
    }
    private void OnDisable()
    {
        actionData.onEnterMicroscopeView -= EnterMicroscopeView;
        actionData.onEnterRoomView -= EnterRoomView;
        actionData.onEnterWikiView -= EnterWikiView;
    }
    private void Update()
    {
        UpdateState();
        SmoothMove();
    }
    private void LateUpdate()
    {
        SetSnapDifferenceGlobalShaderVariable();
    }
    private void Init()
    {
        cam = Camera.main;
        cam.orthographicSize = GetSnappedOrthoSize();
        camData.worldUnitsPerPixel = (cam.orthographicSize * 2) / Screen.height;

        fadeClock = camData.fadeTime;

        Vector3 boundsSize = new Vector3();
        boundsSize.x = cam.orthographicSize * 2 * cam.aspect;
        boundsSize.y = cam.orthographicSize * 2;
        boundsSize.z = cam.farClipPlane + cam.nearClipPlane;
        camData.bounds = new Bounds();
        camData.bounds.size = boundsSize;

        options.SetColors();
    }
    public void SetState(PlayerSO.State newState)
    {
        if (playerData.curState == newState) return;
        ExitState();
        playerData.curState = newState;
        EnterState();
    }

    public void UpdateState()
    {

    }

    public void EnterState()
    {
        switch(playerData.curState)
        {
            case PlayerSO.State.Room:
            {
                transform.position = camData.roomWorldPos;
                FadeIn(FadeOutToVignette);
            }
            break;

            case PlayerSO.State.Microscope:
            {
                transform.position = camData.microscopeWorldPos;
                FadeIn(FadeOutToScope);
            }
            break;

            case PlayerSO.State.Wiki:
            {
                transform.position = camData.wikiWorldPos;
                FadeIn(FadeOutToVignette);
            }
            break;
        }
    }
    public void ExitState()
    {

    }

    public void EnterRoomView()
    {
        SetState(PlayerSO.State.Room);
    }

    public void EnterMicroscopeView()
    {
        SetState(PlayerSO.State.Microscope);
    }

    public void EnterWikiView()
    {
        SetState(PlayerSO.State.Wiki);
    }

    private void SmoothMove()
    {
        camData.preCamWorldPos = camData.curCamWorldPos;
        rawCurWorldPos = Vector3.Lerp(rawCurWorldPos, targetWorldPos, Time.deltaTime * camData.damping);
        camData.curCamWorldPos = GetSnappedPosition(rawCurWorldPos, camData.worldUnitsPerPixel);
        transform.position = camData.curCamWorldPos;
        camData.bounds.center = transform.position;

        Shader.SetGlobalVector("_CamPos", camData.curCamWorldPos);
    }

    private float GetSnappedOrthoSize()
    {
        return (Screen.height * 0.5f / PIXELS_PER_UNIT);
    }
    private void SetSnapDifferenceGlobalShaderVariable()
    {
        Shader.SetGlobalVector("_SnapDiff", rawCurWorldPos - camData.curCamWorldPos);
    }
    public static Vector3 GetSnappedPosition(Vector3 pos, float unitsPerPixel)
    {
        Matrix4x4 W2C = Camera.main.worldToCameraMatrix;
        Matrix4x4 C2W = Camera.main.cameraToWorldMatrix;

        Vector3 camSpace = W2C.MultiplyPoint3x4(pos);
        camSpace.x = Mathf.Round(camSpace.x / unitsPerPixel) * unitsPerPixel;
        camSpace.y = Mathf.Round(camSpace.y / unitsPerPixel) * unitsPerPixel;

        Vector3 snappedPos = C2W.MultiplyPoint3x4(camSpace);
        return snappedPos;
    }
    private void FadeIn(OnFinishFadeIn callBack = null)
    {
        ctsFade?.Cancel();
        ctsFade = new CancellationTokenSource();
        FadingIn(callBack).Forget();
    }
    private void FadeOutToVignette()
    {
        ctsFade?.Cancel();
        ctsFade = new CancellationTokenSource();
        FadingOutToVignette().Forget();
    }
    private void FadeOutToScope()
    {
        ctsFade?.Cancel();
        ctsFade = new CancellationTokenSource();
        FadingOutToScope().Forget();
    }
    private async UniTask FadingIn(OnFinishFadeIn callback)
    {
        try
        {
            while (fadeClock > 0)
            {
                float t = fadeClock / camData.fadeTime;
                t = Curves.EaseOutT(t, 2);
                camData.postProcessMaterial.SetFloat("_Fade", t);
                await UniTask.Yield(ctsFade.Token);
            
                fadeClock -= Time.deltaTime * 10;
            }
            camData.postProcessMaterial.SetFloat("_Fade", 0);
        
            if (callback != null)
            {
                callback();
            }
        }
        catch(OperationCanceledException)
        {

        }
    }
    private async UniTask FadingOutToVignette()
    {
        try
        {
            camData.postProcessMaterial.SetFloat("_SquareToCircle", 0);
            while (fadeClock < camData.fadeTime)
            {
                float t = fadeClock / camData.fadeTime;
                t = Curves.EaseOutT(t, 2);
                camData.postProcessMaterial.SetFloat("_Fade", t);
                await UniTask.Yield(ctsFade.Token);
                fadeClock += Time.deltaTime;
            }
            camData.postProcessMaterial.SetFloat("_Fade", 1);
        }
        catch (OperationCanceledException)
        {

        }
    }
    private async UniTask FadingOutToScope()
    {
        try
        {
            camData.postProcessMaterial.SetFloat("_SquareToCircle", 1);
            while (fadeClock < camData.fadeTime)
            {
                float t = fadeClock / camData.fadeTime;
                t = Curves.EaseOutT(t, 2);
                camData.postProcessMaterial.SetFloat("_Fade", t);
                camData.postProcessMaterial.SetFloat("_Focus", (1.0f - t) * 0.005f);
                await UniTask.Yield(ctsFade.Token);
                fadeClock += Time.deltaTime;
            }
            camData.postProcessMaterial.SetFloat("_Fade", 1);
        }
        catch( OperationCanceledException)
        {

        }
    }

}

