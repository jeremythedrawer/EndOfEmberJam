using TreeEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.InputSystem.InputAction;

public class CursorController : MonoBehaviour
{
    const float VISIBLE_TIMER = 3f;
    const float MOVE_THRESHOLD = 0.01f;

    public static AtlasRenderer PrevRenderer;
    public static AtlasRenderer CursorRenderer;

    public InputSO inputData;
    public CameraData camData;

    public CursorSO cursorData;
    public AudioSO audioData;
    public OptionsSO options;
    public PlayerSO playerData;

    public AtlasRenderer cursorRenderer;
    public AudioSource audioSource;

    [Header("Generated")]

    public float timer;

    public bool cursorIsMoving;
    public bool active;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (active)
        {
            if (inputData.mouseLeftUp && cursorData.isHovering)
            {
                audioSource.PlayOneShot(audioData.cursorClick);
            }
            cursorRenderer.enabled = true;
            transform.position = inputData.mouseWorldPos;

            cursorData.cursorBounds = cursorRenderer.GetBounds();
        }
    }
    private void LateUpdate()
    {
        cursorData.CheckButtonResults();

        if (inputData.mouseDelta.sqrMagnitude < MOVE_THRESHOLD && !inputData.mouseLeftHold && !cursorData.isHovering)
        {
            cursorIsMoving = false;
            timer += Time.deltaTime;

            if (timer > VISIBLE_TIMER)
            {
                if (active)
                {
                    cursorRenderer.enabled = false;
                    active = false;
                }
            }
        }
        else
        {
            cursorIsMoving = true;
            if (!active)
            {
                cursorRenderer.enabled = true;
                timer = 0;
                active = true;
            }
        }
        if (cursorData.changeButton)
        {
            cursorRenderer.UpdateSpriteInputsByIndex((int)cursorData.curCursorSpriteType);
            audioSource.PlayOneShot(audioData.cursorHover);

            cursorData.changeButton = false;
        }
        else if (!cursorData.isHovering)
        {
            if (cursorRenderer.spriteIndex != (int)CursorSO.CursorSpriteType.Arrow)
            {
                cursorRenderer.UpdateSpriteInputsByIndex((int)CursorSO.CursorSpriteType.Arrow);
            }
        }
    }

    private void PlayAudioOnClick(CallbackContext ctx)
    {
        audioSource.PlayOneShot(audioData.cursorClick);
    }
    private void Init()
    {
        Cursor.visible = false;
        CursorRenderer = cursorRenderer;

        cursorData.curCursorSpriteType = CursorSO.CursorSpriteType.Point;

        playerData.curState = PlayerSO.State.Room;
    }
    private void UpdateVolume()
    {
        audioSource.volume = audioData.soundEffectsVolume;
    }
}
