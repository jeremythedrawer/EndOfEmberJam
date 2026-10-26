using UnityEngine;
using System.Collections;
using Proselyte.Sigils;

public class MoldController : MonoBehaviour
{
    public GameObject[] moldStages = new GameObject[8];
    public int currentStage = 0;

    void Awake()
    {
        UpdateMoldStage();
    }

    public void UpdateMoldStage()
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
    }
}
