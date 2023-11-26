using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(KeywordManager))]
public class KeywordManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        KeywordManager myScript = (KeywordManager)target;
        if (GUILayout.Button("Get Keywords"))
        {
            myScript.GetKeywords();
        }
    }
}