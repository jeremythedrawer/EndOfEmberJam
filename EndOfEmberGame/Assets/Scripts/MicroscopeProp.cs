using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using static Atlas;
using static AtlasRendering;

public class MicroscopeProp : MonoBehaviour
{
    public MicroscopeSO microscopeData;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        for (int i = 0; i < microscopeData.petriDishDatas.Length; i++)
        {
            PetriDishSO petriDish = microscopeData.petriDishDatas[i];
            petriDish.index = i;
        }
        microscopeData.activePetriDishIndex = -1;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MicroscopeProp))]
public class MicropePropEditor : Editor
{
    SphereBoundsHandle boundsHandle = new SphereBoundsHandle();

    private void OnSceneGUI()
    {

        MicroscopeProp microscopeProp = (MicroscopeProp)target;

        if (microscopeProp.microscopeData == null) return;

        if (Selection.activeGameObject == microscopeProp.gameObject)
        {
            EditorGUI.BeginChangeCheck();

            boundsHandle.center = microscopeProp.microscopeData.triggerCircle.center;
            boundsHandle.radius = microscopeProp.microscopeData.triggerCircle.radius;
        }

        boundsHandle.SetColor(Color.red);
        boundsHandle.DrawHandle();

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(microscopeProp, "Resize Bounds");
            microscopeProp.microscopeData.triggerCircle.center.x = boundsHandle.center.x;
            microscopeProp.microscopeData.triggerCircle.center.y = boundsHandle.center.y;
            microscopeProp.microscopeData.triggerCircle.center.z = microscopeProp.transform.position.z - 0.1f;
            microscopeProp.microscopeData.triggerCircle.radius = boundsHandle.radius;
        }
    }
}
#endif
