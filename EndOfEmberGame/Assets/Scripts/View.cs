using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
using static Biology;
public class View : MonoBehaviour
{
    public CameraSO camData;
    public ViewType viewType;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        switch(viewType)
        {
            case ViewType.Room:
            {
                camData.roomWorldPos = transform.position;
            }
            break;

            case ViewType.PetriDishTable:
            {
                camData.petriDishTableWorldPos = transform.position;
            }
            break;

            case ViewType.Microscope:
            {
                camData.microscopeWorldPos = transform.position;
            }
            break;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(View))]
public class ViewEditor : Editor
{
    private void OnSceneGUI()
    {
        View view = (View)target;

        if (view.camData == null) return;

        Vector3 camBoundsPosition = view.transform.position;
        Vector3 camBoundsSize = new Vector3();
        camBoundsSize.x = view.camData.bounds.size.x;
        camBoundsSize.y = view.camData.bounds.size.y;
        camBoundsSize.z = 0;

        Handles.color = Color.blue;
        Handles.DrawWireCube(camBoundsPosition, camBoundsSize);

        GUIContent labelContent = new GUIContent();
        labelContent.text = view.viewType.ToString();

        GUIStyle labelStyle = new GUIStyle();
        labelStyle.alignment = TextAnchor.MiddleCenter;
        labelStyle.normal.textColor = Handles.color;
        Handles.Label(camBoundsPosition, labelContent, labelStyle);
    }
}
#endif
