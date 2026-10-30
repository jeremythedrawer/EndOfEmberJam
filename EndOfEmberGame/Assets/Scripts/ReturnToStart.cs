using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToStart : MonoBehaviour
{
    public void ReturnToStartFunction()
    {
        SceneManager.LoadScene(0);
    }
}
