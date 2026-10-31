using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitleScreen : MonoBehaviour, IPointerClickHandler
{
    bool ready;
    [SerializeField]
    TMP_Text text;
    [SerializeField] VideoPlayer videoPlayer;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ready)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void OpenSetting()
    {
        SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
    }

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(LoadAssets());
#else
        text.text = "Click To Start";
        ready = true;
        videoPlayer.Play();
#endif
    }

    IEnumerator LoadAssets()
    {
        string path = Path.Combine(Application.streamingAssetsPath);
        print(path);

        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(path);
        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.Success:
                print("success");
                break;
            case UnityWebRequest.Result.ConnectionError:
                print("connection error");
                break;
            case UnityWebRequest.Result.ProtocolError:
                print("protocol error");
                break;
            case UnityWebRequest.Result.DataProcessingError:
                print("data error");
                break;
        }

        // Get downloaded asset bundle
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

        if (bundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }

        text.text = "Click To Start";
        ready = true;
        videoPlayer.Play();
    }
}
