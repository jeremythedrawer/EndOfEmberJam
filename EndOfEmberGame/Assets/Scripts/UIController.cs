using UnityEngine;

public class UIController : MonoBehaviour
{
    public AtlasTextRenderer headerRenderer;



    public TextButton leftButton;
    public TextButton rightButton;

    public ActionSO actionData;
    public PlayerSO playerData;
    public CameraSO camData;
    private void Start()
    {
        InitButtons();
    }
    private void Update()
    {
        UpdateButtons();
    }
    private void InitButtons()
    {
        void LeftButtonMouseUp()
        {
            leftButton.MouseUp();

            switch (playerData.curState)
            {
                case PlayerSO.State.Room:
                {
                    leftButton.textRenderer.SetText("");
                    rightButton.textRenderer.SetText("Room");
                    
                    headerRenderer.SetText("Wiki");
                    
                    actionData.onToWikiView?.Invoke();
                }
                break;

                case PlayerSO.State.Wiki:
                {
                }
                break;

                case PlayerSO.State.Microscope:
                {
                    leftButton.textRenderer.SetText("Wiki");
                    rightButton.textRenderer.SetText("Microscope");

                    headerRenderer.SetText("Room");
                    
                    actionData.onToRoomView?.Invoke();
                }
                break;
            }

        }
        void RightButtonMouseUp()
        {
            rightButton.MouseUp();

            switch (playerData.curState)
            {
                case PlayerSO.State.Room:
                {
                    leftButton.textRenderer.SetText("Room");
                    rightButton.textRenderer.SetText("");

                    headerRenderer.SetText("Microscope");

                    actionData.onToMicroscopeView?.Invoke();
                }
                break;

                case PlayerSO.State.Wiki:
                {
                    leftButton.textRenderer.SetText("Wiki");
                    rightButton.textRenderer.SetText("Microscope");

                    headerRenderer.SetText("Room");

                    actionData.onToRoomView?.Invoke();
                }
                break;

                case PlayerSO.State.Microscope:
                {

                }
                break;
            }
        }

        leftButton.InitButton(onMouseUp: LeftButtonMouseUp);
        rightButton.InitButton(onMouseUp: RightButtonMouseUp);
    }
    private void UpdateButtons()
    {
        switch (playerData.curState)
        {
            case PlayerSO.State.Room:
            {
                leftButton.UpdateButton();
                rightButton.UpdateButton();
            }
            break;

            case PlayerSO.State.Wiki:
            {
                rightButton.UpdateButton();
            }
            break;

            case PlayerSO.State.Microscope:
            {
                leftButton.UpdateButton();
            }
            break;
        }
    }
}
