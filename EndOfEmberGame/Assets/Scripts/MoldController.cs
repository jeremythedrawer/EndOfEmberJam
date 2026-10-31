using UnityEngine;
using System.Collections;
using Proselyte.Sigils;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoldController : MonoBehaviour
{
    public GameObject[] moldStages = new GameObject[7];
    public PlayerSO playerData;
    public GameEvent moldStageIncreaseEvent;
    public GameEvent endGameEvent;
    public Button moldAdvanceButton;

    void Awake()
    {
        MoldStageUpdate();
    }

    private void Start()
    {
        moldAdvanceButton.enabled = false;
    }

    private void OnEnable()
    {
        MoldStageUpdate();
        moldStageIncreaseEvent.RegisterListener(OnMoldStageIncrease);
        endGameEvent.RegisterListener(() => StartCoroutine(GameEndSequence()));
    }

    private void OnDisable()
    {
        moldStageIncreaseEvent.UnregisterListener(OnMoldStageIncrease);
    }

    private void MoldStageUpdate()
    {
        
        for (int i = 0; i < moldStages.Length; i++)
        {
            if(i == playerData.moldStageCurrent - 1 && playerData.moldStageCurrent != 0)
            {
                moldStages[i].SetActive(true);
                continue;
            }

            moldStages[i].SetActive(false);
        }

        if(playerData.moldStageCurrent >= 7)
        {
            endGameEvent.Raise();
        }
    }

    private IEnumerator GameEndSequence()
    {
        moldAdvanceButton.enabled = false;
        yield return new WaitForSeconds(3f);

        // final stage, remove button interactivity for wall and trigger credits to roll
        SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
    }

    public void OnMoldStageIncrease()
    {
        MoldStageUpdate();
    }
}
