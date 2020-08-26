using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace SkillSystem
{
    [CustomEditor(typeof(Skill))]
    public class SkillEditor : Editor
    {
        Skill skill;
        int selection;

        public override void OnInspectorGUI()
        {
            skill = (Skill)target;
            base.OnInspectorGUI();

            skill.allComponents = new List<ISkillComponent>();
            skill.allComponents.AddRange(skill.animationComponents);

            for(int i = 0; i < skill.allComponents.Count; i++)
            {
                skill.allComponents[i].ShowGUI();
                skill.allComponents[i].UpdateDescription();
            }

            AddComponentButton();
        }


        private void AddComponentButton()
        {
            EditorGUILayout.Space(10);

            string[] options = { "Animation", "Skill Object"};

            EditorGUILayout.BeginHorizontal();
            selection = EditorGUILayout.Popup(selection, options);
            GUILayout.Button("Add Component");
            EditorGUILayout.EndHorizontal();
        }

        private void IncrementOrDecrementButton(int index)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("^"))
            {

            }
            if (GUILayout.Button("v"))
            {

            }
            GUILayout.Space(1000);

            EditorGUILayout.EndHorizontal();
        }

        private void IncrementIndex()
        {

        }
    }

    
}
