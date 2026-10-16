using UnityEngine;

using static Atlas;
public class CameraController : MonoBehaviour
{
    public CameraSO camData;
    public ActionSO actionData;
    public PlayerSO playerData;

    [Header("Generated")]
    
    public Camera cam;

    public Vector3 rawCurWorldPos;
    public Vector3 targetWorldPos;


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

        Vector3 boundsSize = new Vector3();
        boundsSize.x = cam.orthographicSize * 2 * cam.aspect;
        boundsSize.y = cam.orthographicSize * 2;
        boundsSize.z = cam.farClipPlane + cam.nearClipPlane;
        camData.bounds = new Bounds();
        camData.bounds.size = boundsSize;
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
            }
            break;

            case PlayerSO.State.Microscope:
            {
                transform.position = camData.microscopeWorldPos;
            }
            break;

            case PlayerSO.State.Wiki:
            {
                transform.position = camData.wikiWorldPos;
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
}

