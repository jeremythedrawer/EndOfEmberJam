using UnityEngine;

public class UIController : MonoBehaviour
{
    public AtlasTextRenderer headerRenderer;

    public TextButton leftButton;
    public TextButton rightButton;

    public ActionSO actionData;
    public PlayerSO playerData;
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

                    playerData.curState = PlayerSO.State.Wiki;
                    headerRenderer.SetText("Wiki");
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

                    playerData.curState = PlayerSO.State.Room;
                    headerRenderer.SetText("Room");
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
                    playerData.curState = PlayerSO.State.Microscope;
                    headerRenderer.SetText("Microscope");
                }
                break;

                case PlayerSO.State.Wiki:
                {
                    leftButton.textRenderer.SetText("Wiki");
                    rightButton.textRenderer.SetText("Microscope");
                    playerData.curState = PlayerSO.State.Room;
                    headerRenderer.SetText("Room");
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
