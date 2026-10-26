using Proselyte.Sigils;
using UnityEngine;
using UnityEngine.InputSystem;

using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public InputSO inputData;
    public PlayerInput playerInput;

    public InputAction cursorLeftDownAction;
    public InputAction anyKeyDownAction;

    public GameEvent wikiConfirmPageEvent;
    public GameEvent subtitleConfirmEvent;

    private void Awake()
    {
        // Init inputs
        
        cursorLeftDownAction = playerInput.actions["Player/CursorLeftDown"];
        cursorLeftDownAction.started += OnStartLeftMouse;
        cursorLeftDownAction.performed += OnPerformLeftMouse;
        cursorLeftDownAction.canceled += OnCancelLeftMouse;

        anyKeyDownAction = playerInput.actions["Player/AnyKeyDown"];
        anyKeyDownAction.started += OnAnyKeyDownCool;

    }

    private void Start()
    {
        inputData.mouseScreenPos.z = 0.25f;
    }

    private void Update()
    {
        // Handle mouse move
        Vector2 screenPos = Mouse.current.position.ReadValue();
        inputData.mouseScreenPos.x = Mathf.Clamp(screenPos.x, 0.0f, Screen.width);
        inputData.mouseScreenPos.y = Mathf.Clamp(screenPos.y, 0.0f, Screen.height);
        inputData.mouseWorldPos = Camera.main.ScreenToWorldPoint(inputData.mouseScreenPos);
        inputData.mouseDelta = Mouse.current.delta.ReadValue();
    }
    private void LateUpdate()
    {
        inputData.mouseLeftDown = false;
        inputData.mouseLeftUp = false;
    }

    private void OnStartLeftMouse(CallbackContext ctx)
    { 
        inputData.mouseLeftDown = true;
    }
    private void OnPerformLeftMouse(CallbackContext ctx)
    {
        inputData.mouseLeftHold = true;
    }
    private void OnCancelLeftMouse(CallbackContext ctx)
    {
        inputData.mouseLeftUp = true;
        inputData.mouseLeftHold = false;
    }

    private void OnAnyKeyDownCool(CallbackContext ctx)
    {
        wikiConfirmPageEvent.Raise();
        subtitleConfirmEvent.Raise();
    }
}
