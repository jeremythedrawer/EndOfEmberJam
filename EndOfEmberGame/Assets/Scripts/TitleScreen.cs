using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleScreen : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] SpriteAnimator spriteAnimator;
    void Start()
    {
        StartCoroutine(spriteAnimator.playAnimation());
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSetting()
    {
        SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
    }
}
