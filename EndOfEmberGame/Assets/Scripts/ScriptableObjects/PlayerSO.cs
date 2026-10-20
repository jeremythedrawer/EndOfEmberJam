using UnityEngine;

using static Biology;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Custom / Player SO")]
public class PlayerSO : ScriptableObject
{
    public ViewType curView;
    public ViewType preView;
}
