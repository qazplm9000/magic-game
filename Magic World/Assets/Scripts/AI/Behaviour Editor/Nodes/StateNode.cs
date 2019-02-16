using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Behaviour;
using UnityEditor;

namespace BehaviourEditor
{
    public class StateNode : BaseNode
    {
        bool collapse;
        public State currentState;
        public State previousState;

        List<BaseNode> dependencies = new List<BaseNode>();

        public override void DrawWindow()
        {
            if (currentState == null)
            {
                EditorGUILayout.LabelField("Add state to modify: ");
            }
            else {
                if (!collapse)
                {
                    windowRect.height = 300;
                }
                else {
                    windowRect.height = 100;
                }

                collapse = EditorGUILayout.Toggle(" ", collapse);
            }

            currentState = (State)EditorGUILayout.ObjectField(currentState, typeof(State), false);

            if (previousState != currentState) {
                previousState = currentState;
                ClearReferences();

                for (int i = 0; i < currentState.transitions.Count; i++) {
                    dependencies.Add(BehaviourEditor.AddTransitionNode(i, currentState.transitions[i], this));
                }
            }
        }

        public override void DrawCurves() {

        }

        public Transition AddTransition() {
            return currentState.AddTransition();
        }

        public void ClearReferences(){
            BehaviourEditor.ClearWindowsFromList(dependencies);
            dependencies.Clear();
        }

    }
}