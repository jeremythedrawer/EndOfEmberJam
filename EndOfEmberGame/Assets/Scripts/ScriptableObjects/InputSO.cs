using UnityEngine;

[CreateAssetMenu(fileName = "InputSO", menuName = "Custom / Input SO")]
public class InputSO : ScriptableObject
{
    public Vector3 mouseScreenPos;
    public Vector3 mouseWorldPos;
    public Vector3 mouseDelta;   
    public bool mouseLeftDown;
    public bool mouseLeftHold;
    public bool mouseLeftUp;

}
