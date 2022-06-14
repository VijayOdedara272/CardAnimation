using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(editorinharite))]
public class EditorCode : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        editorinharite ei = (editorinharite)target;

        if (GUILayout.Button("Set Positions"))
        {
            ei.SetPos();
        }
        if (GUILayout.Button("EnterPosintoArray"))
        {
            ei.EnterPosintoArray();
        }
        if (GUILayout.Button("SetHorizontalPos"))
        {
            ei.SetHorizontalPos();
        }
    }
}
