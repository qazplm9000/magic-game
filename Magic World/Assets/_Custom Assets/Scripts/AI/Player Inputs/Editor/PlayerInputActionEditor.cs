using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace InputSystem
{
    [CustomEditor(typeof(PlayerAction), true)]
    public class PlayerInputActionEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            PlayerAction action = (PlayerAction)target;

            EditorStyles.textField.wordWrap = true;
            EditorGUILayout.LabelField("Description");
            action.description = EditorGUILayout.TextArea(action.description);

            base.OnInspectorGUI();
        }
    }
}