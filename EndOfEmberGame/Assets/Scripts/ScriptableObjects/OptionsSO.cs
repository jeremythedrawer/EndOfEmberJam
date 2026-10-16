using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "OptionsSO", menuName = "Custom / Options SO")]
public class OptionsSO : ScriptableObject
{
    public Color darkColor;
    public Color lightColor;


    public int darkColorID;
    public int lightColorID;
    public void SetColors()
    {
        darkColorID = Shader.PropertyToID("_DarkColor");
        lightColorID = Shader.PropertyToID("_LightColor");
        Shader.SetGlobalColor(darkColorID, darkColor.linear);
        Shader.SetGlobalColor(lightColorID, lightColor.linear);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(OptionsSO))]
public class OptionsSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        OptionsSO options = (OptionsSO)target;
        GUIContent setGlobalVariableContent = new GUIContent("Set Global Shader Variables");

        if (GUILayout.Button(setGlobalVariableContent))
        {
            options.SetColors();
        }
    }
}
#endif