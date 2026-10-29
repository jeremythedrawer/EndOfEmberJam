using UnityEngine;
using System.Collections;
using Proselyte.Sigils;

public class MoldController : MonoBehaviour
{
    public GameObject[] moldStages = new GameObject[8];
    public int currentStage = 0;
    public AudioSource moldAudioSource;

    public AudioClip[] audioClips = new AudioClip[8];

    void Awake()
    {
        UpdateMoldStage();
    }

    private void UpdateMoldStage()
    {
        for (int i = 0; i < moldStages.Length; i++)
        {
            if (i == currentStage) continue;

            moldStages[i].SetActive(false);
        }
    }

    public void IncreaseMoldStage()
    {
        currentStage += 1;

        switch(currentStage)
        {
            case 0:
                moldAudioSource.PlayOneShot(audioClips[0]);
                break;
            case 1:
                moldAudioSource.PlayOneShot(audioClips[1]);
                break;
            case 2:
                moldAudioSource.PlayOneShot(audioClips[2]);
                break;
            case 3:
                moldAudioSource.PlayOneShot(audioClips[3]);
                break;
            case 4:
                moldAudioSource.PlayOneShot(audioClips[4]);
                break;
            case 5:
                moldAudioSource.PlayOneShot(audioClips[5]);
                break;
            case 6:
                moldAudioSource.PlayOneShot(audioClips[6]);
                break;
            case 7:
                moldAudioSource.PlayOneShot(audioClips[7]);
                break;
            case 8:
                moldAudioSource.PlayOneShot(audioClips[8]);
                break;
            default:
                break;
        }
        
        UpdateMoldStage();
    }
}
