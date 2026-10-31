using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] float framePerSecond;
    [SerializeField] Sprite[] sprites;
    [SerializeField] bool loop;
    private Image m_image;

    void Start()
    {
        m_image = GetComponent<Image>();
    }

    public IEnumerator playAnimation()
    {
        WaitForSeconds wait = new WaitForSeconds(1f / framePerSecond);
        int index = 0;
        while (true)
        {
            m_image.sprite = sprites[index];
            index++;
            if (index >= sprites.Length)
            {
                if (loop)
                { index = 0; }
                else
                {
                    break;
                }
            }
            yield return wait;
        }
    }
}
