using UnityEngine;
using System.Collections;
using Proselyte.Sigils;

public class MoldController : MonoBehaviour
{
    public GameObject[] moldStages = new GameObject[8];
    public PlayerSO playerData;
    public GameEvent moldStageIncreaseEvent;

    void Awake()
    {
        MoldStageUpdate();
    }

    private void OnEnable()
    {
        MoldStageUpdate();
        moldStageIncreaseEvent.RegisterListener(OnMoldStageIncrease);
    }

    private void OnDisable()
    {
        moldStageIncreaseEvent.UnregisterListener(OnMoldStageIncrease);
    }

    private void MoldStageUpdate()
    {
        for (int i = 0; i < moldStages.Length; i++)
        {
            if(i == playerData.moldStageCurrent)
            {
                moldStages[i].SetActive(true);
                continue;
            }

            moldStages[i].SetActive(false);
        }
    }

    public void OnMoldStageIncrease()
    {
        MoldStageUpdate();
    }
}
