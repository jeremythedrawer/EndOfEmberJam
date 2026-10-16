using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum State
    { 
        None,
        Room,
        Microscope,
        Wiki,
    }

    public CameraSO camData;
    public ActionSO actionData;

    [Header("Generated")]
    public State curState;
    private void Start()
    {
        
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
    }

    public void SetState(State newState)
    {
        if (curState == newState) return;
        ExitState();
        curState = newState;
        EnterState();
    }

    public void UpdateState()
    {

    }

    public void EnterState()
    {
        switch(curState)
        {
            case State.Room:
            {
                transform.position = camData.roomWorldPos;
            }
            break;

            case State.Microscope:
            {
                transform.position = camData.microscopeWorldPos;
            }
            break;

            case State.Wiki:
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
        SetState(State.Room);
    }

    public void EnterMicroscopeView()
    {
        SetState(State.Microscope);
    }

    public void EnterWikiView()
    {
        SetState(State.Wiki);
    }
}

