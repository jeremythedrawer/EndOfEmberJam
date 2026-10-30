using Proselyte.Sigils;
using UnityEngine;

public class PosterController : MonoBehaviour
{
    public GameObject posterGO;
    public PlayerSO playerData;
    
    private void OnEnable()
    {
        if(playerData.researchedMitochondria)
        {
            posterGO.SetActive(true);
        }
        else
        {
            posterGO.SetActive(false);
        }
    }
}
