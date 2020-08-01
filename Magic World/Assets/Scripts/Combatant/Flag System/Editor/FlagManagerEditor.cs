using StateSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlagManager))]
public class FlagManagerEditor : Editor
{
    FlagManager fm;

    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        fm = (FlagManager)target;

        fm.foldout = EditorGUILayout.Foldout(fm.foldout, "Flags", true);

        EditorGUI.indentLevel++;
        if (fm.foldout)
        {
            FlagGUI();
        }
        EditorGUI.indentLevel--;

        
    }

    private void FlagGUI()
    {
        string[] flagNames = FlagManager.flagNames;
        for (int i = 0; i < fm.flagValues.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(flagNames[i]);
            bool temp = EditorGUILayout.Toggle(fm.flagValues[i]);
            
            if(temp != fm.flagValues[i])
            {
                EditorUtility.SetDirty(target);
                fm.flagValues[i] = temp;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
