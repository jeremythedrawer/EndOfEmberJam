using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DisplaySettings : MonoBehaviour
{
    LiftGammaGain liftGammaGain;
    [SerializeField] PlayerInput input;
    [SerializeField] VolumeProfile profile;

    void Start()
    {
        profile.TryGet<LiftGammaGain>(out liftGammaGain);
        SettingSO.ApplySetting();
    }


    public void OpenSetting(InputAction.CallbackContext context)
    {
        if (SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
        }
    }

    void OnEnable()
    {
        input.actions["UI/Setting"].performed += OpenSetting;
        input.actions["UI/Setting"].Enable();
        SettingSO.onApplySetting += UpdateBrightness;
    }
    void OnDisable()
    {
        input.actions["UI/Setting"].performed -= OpenSetting;
        SettingSO.onApplySetting -= UpdateBrightness;
    }

    void UpdateBrightness()
    {
        liftGammaGain.gain.value = new Vector4(1,1,1,SettingSO.brightness);
    }
}
