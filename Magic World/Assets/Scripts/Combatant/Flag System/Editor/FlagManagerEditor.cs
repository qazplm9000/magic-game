using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class FlagManagerEditor : Editor
{
    FlagManager fm;

    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        fm = (FlagManager)target;

        EditorGUILayout.Foldout(fm.foldout, "Flags");

        EditorGUI.indentLevel++;
        FlagGUI();
        EditorGUI.indentLevel--;

        EditorUtility.SetDirty(target);
    }

    private void FlagGUI()
    {
        Flag[] flagEnum = (Flag[])Enum.GetValues(typeof(Flag));
        for (int i = 0; i < fm.flagValues.Count; i++)
        {
            EditorGUI.Toggle()
        }
    }
}
