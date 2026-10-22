using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Unity.Collections;
using UnityEngine;

using static Atlas;
using static Biology;
public class CameraController : MonoBehaviour
{
    public delegate void OnFinishFadeIn();

    public AudioSource audioSource;

    public CameraSO camData;
    public ActionSO actionData;
    public PlayerSO playerData;
    public OptionsSO options;
    public AudioSO audioData;
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
        actionData.onToMicroscopeView += EnterMicroscopeView;
        actionData.onToRoomView += EnterRoomView;
        actionData.onToPetriDishTableView += EnterPetriDishView;
    }
    private void OnDisable()
    {
        actionData.onToMicroscopeView -= EnterMicroscopeView;
        actionData.onToRoomView -= EnterRoomView;
        actionData.onToPetriDishTableView -= EnterPetriDishView;
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

        fadeClock = audioData.microscopeView001.length;

        Vector3 boundsSize = new Vector3();
        boundsSize.x = cam.orthographicSize * 2 * cam.aspect;
        boundsSize.y = cam.orthographicSize * 2;
        boundsSize.z = cam.farClipPlane + cam.nearClipPlane;
        camData.bounds = new Bounds();
        camData.bounds.size = boundsSize;

        rawCurWorldPos.z = transform.position.z;
        targetWorldPos.z += transform.position.z;

        options.SetColors();

        int numSamples = audioData.microscopeView001.samples * audioData.microscopeView001.channels;
        NativeArray<float> microscopeViewSamples = new NativeArray<float>(numSamples, Allocator.Temp);
    }
    public void SetState(ViewType newState)
    {
        if (playerData.curView == newState) return;
        ExitState();
        playerData.preView = playerData.curView;
        playerData.curView = newState;
        EnterState();
    }

    public void UpdateState()
    {

    }

    public void EnterState()
    {
        switch(playerData.curView)
        {
            case ViewType.Room:
            {
                FadeIn(FadeToRoom);
            }
            break;

            case ViewType.Microscope:
            {
                FadeIn(FadeToMicroscope);
            }
            break;

            case ViewType.PetriDishTable:
            {
                FadeIn(FadeToPetryDishTable);
            }
            break;
        }
    }
    public void ExitState()
    {

    }

    public void EnterRoomView()
    {
        SetState(ViewType.Room);
    }

    public void EnterMicroscopeView()
    {
        SetState(ViewType.Microscope);
    }

    public void EnterPetriDishView()
    {
        SetState(ViewType.PetriDishTable);
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
    private void FadeToRoom()
    {
        targetWorldPos = camData.roomWorldPos;
        rawCurWorldPos = targetWorldPos;
        camData.bounds.center = targetWorldPos;
        ctsFade?.Cancel();
        ctsFade = new CancellationTokenSource();
        FadingOutToVignette().Forget();
        
        actionData.onEnterRoomView?.Invoke();

    }
    private void FadeToPetryDishTable()
    {
        targetWorldPos = camData.petriDishTableWorldPos;
        rawCurWorldPos = targetWorldPos;
        camData.bounds.center = targetWorldPos;

        ctsFade?.Cancel();
        ctsFade = new CancellationTokenSource();
        FadingOutToVignette().Forget();

        actionData.onEnterPetriDishView?.Invoke();

        if (playerData.preView == ViewType.Microscope)
        {
            actionData.onExitMicroscopeView?.Invoke();
        }
    }
    private void FadeToMicroscope()
    {
        targetWorldPos = camData.microscopeWorldPos;
        rawCurWorldPos = targetWorldPos;
        camData.bounds.center = targetWorldPos;

        ctsFade?.Cancel();
        ctsFade = new CancellationTokenSource();
        FadingOutToMicroscope().Forget();


        audioSource.PlayOneShot(audioData.microscopeView001);

        actionData.onEnterMicroscopeView?.Invoke();
    }
    private async UniTask FadingIn(OnFinishFadeIn callback)
    {
        try
        {
            while (fadeClock > 0)
            {
                float fade = fadeClock / audioData.microscopeView001.length;
                fade = Curves.EaseOutT(fade, 2);
                camData.postProcessMaterial.SetFloat("_Fade", fade);
                float brightness = fade;
                camData.postProcessMaterial.SetFloat("_Brightness", brightness);
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
            while (fadeClock < audioData.microscopeView001.length)
            {
                float fade = fadeClock / audioData.microscopeView001.length;
                fade = Curves.EaseOutT(fade, 2);
                camData.postProcessMaterial.SetFloat("_Fade", fade);
                await UniTask.Yield(ctsFade.Token);
                fadeClock += Time.deltaTime;
            }
            camData.postProcessMaterial.SetFloat("_Fade", 1);
        }
        catch (OperationCanceledException)
        {

        }
    }
    private async UniTask FadingOutToMicroscope()
    {
        try
        {
            int clickIndex = 0;
            while (clickIndex < audioData.microScopeClickTimes.Length)
            {
                float fade = fadeClock / audioData.microscopeView001.length;
                camData.postProcessMaterial.SetFloat("_Fade", fade);
                float brightness = fade;
                float focus = (1.0f - fade) * 0.01f;


                camData.postProcessMaterial.SetFloat("_Brightness", brightness);
                camData.postProcessMaterial.SetFloat("_Focus", focus);
                
                while(fadeClock < audioData.microScopeClickTimes[clickIndex])
                {
                    fadeClock += Time.deltaTime;
                    await UniTask.Yield();
                }
                clickIndex++;

                await UniTask.Yield(ctsFade.Token);
            }
            while (fadeClock < audioData.microscopeView001.length)
            {
                float fade = fadeClock / audioData.microscopeView001.length;
                camData.postProcessMaterial.SetFloat("_Fade", fade);
                float brightness = fade;
                float focus = (1.0f - fade) * 0.01f;

                camData.postProcessMaterial.SetFloat("_Brightness", brightness);
                camData.postProcessMaterial.SetFloat("_Focus", focus);

                await UniTask.Yield();

                fadeClock += Time.deltaTime;
            }
            camData.postProcessMaterial.SetFloat("_Fade", 1);
            camData.postProcessMaterial.SetFloat("_Focus", 0);
        }
        catch( OperationCanceledException)
        {

        }
    }

}

