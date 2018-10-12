using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Behaviour;

namespace BehaviourEditor
{
    public class BehaviourEditor : EditorWindow
    {
        
        #region Variables
        static List<BaseNode> windows = new List<BaseNode>();
        Vector3 mousePosition;
        bool makeTransition;
        bool clickedOnWindow;
        BaseNode selectedNode;

        public enum UserActions {
            addState,
            addTransitionNode,
            deleteNode,
            commentNode
        }
        #endregion



        #region init
        [MenuItem("Behaviour Editor/Editor")]
        static void ShowEditor() {
            BehaviourEditor editor = EditorWindow.GetWindow<BehaviourEditor>();
            editor.minSize = new Vector2(800, 600);
        }
        #endregion



        #region GUI Methods
        private void OnGUI()
        {
            Event e = Event.current;
            mousePosition = e.mousePosition;
            UserInput(e);
            DrawWindows();
        }

        private void OnEnable()
        {
            //windows.Clear();
        }

        void DrawWindows()
        {
            BeginWindows();

            foreach (BaseNode node in windows) {
                node.DrawCurves();
            }

            for (int i = 0; i < windows.Count; i++) {
                windows[i].windowRect = GUI.Window(i, windows[i].windowRect, DrawNodeWindow, windows[i].windowTitle);
            }

            EndWindows();
        }

        void DrawNodeWindow(int id) {
            windows[id].DrawWindow();
            GUI.DragWindow();
        }

        void UserInput(Event e) {
            if (e.button == 1 && !makeTransition) {
                if (e.type == EventType.MouseDown) {
                    RightClick(e);
                }
            }

            if (e.button == 0 && !makeTransition) {
                if (e.type == EventType.MouseDown) {

                }
            }
        }


        void RightClick(Event e) {
            selectedNode = null;
            for (int i = 0; i < windows.Count; i++) {
                if (windows[i].windowRect.Contains(e.mousePosition)) {
                    clickedOnWindow = true;
                    selectedNode = windows[i];
                    break;
                }
            }

            //Allows user to modify a node if they click on a window
            //Add a new node otherwise
            if (clickedOnWindow)
            {
                ModifyNode(e);
            }
            else {
                AddNewNode(e);
            }
        }

        void AddNewNode(Event e) {
            GenericMenu menu = new GenericMenu();
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Add State"), false, ContextCallback, UserActions.addState);
            menu.AddItem(new GUIContent("Add Comment"), false, ContextCallback, UserActions.commentNode);
            menu.ShowAsContext();
            e.Use();
        }

        void ModifyNode(Event e) {
            GenericMenu menu = new GenericMenu();


            if (selectedNode is StateNode)
            {
                StateNode stateNode = (StateNode)selectedNode;
                if (stateNode.currentState != null)
                {
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Add Transition"), false, ContextCallback, UserActions.addTransitionNode);
                }
                else
                {
                    menu.AddSeparator("");
                    menu.AddDisabledItem(new GUIContent("Add Transition"));
                }
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete State"), false, ContextCallback, UserActions.deleteNode);
            }
            else if (selectedNode is CommentNode) {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);
            }


            menu.ShowAsContext();
            e.Use();
        }

        void ContextCallback(object o) {
            UserActions a = (UserActions)o;

            switch (a)
            {
                case UserActions.addState:
                    StateNode stateNode = new StateNode
                    {
                        windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 300),
                        windowTitle = "State"
                    };
                    windows.Add(stateNode);
                    break;
                case UserActions.addTransitionNode:
                    if (selectedNode is StateNode) {
                        StateNode fromState = (StateNode)selectedNode;
                        Transition transition = fromState.AddTransition();
                        AddTransitionNode(fromState.currentState.transitions.Count, transition, fromState);
                    }
                    break;
                case UserActions.commentNode:
                    CommentNode commentNode = new CommentNode
                    {
                        windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 100),
                        windowTitle = "Comment"
                    };
                    windows.Add(commentNode);
                    break;
                case UserActions.deleteNode:
                    if (selectedNode is StateNode) {
                        StateNode target = (StateNode)selectedNode;
                        target.ClearReferences();
                        windows.Remove(target);
                    }else if (selectedNode is TransitionNode) {

                    }else if (selectedNode is CommentNode) {
                        windows.Remove(selectedNode);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion



        #region Helper Methods

        public static TransitionNode AddTransitionNode(int index, Transition transition, StateNode from) {
            Rect fromRect = from.windowRect;
            fromRect.x += 50;
            float targetY = fromRect.y - fromRect.height;

            if (from.currentState != null) {
                targetY += (index * 100);
            }

            fromRect.y = targetY;

            TransitionNode transitionNode = CreateInstance<TransitionNode>();
            transitionNode.Init(from, transition);
            transitionNode.windowRect = new Rect(fromRect.x + 200 + 100, fromRect.y + (fromRect.height * 0.7f), 200, 80);
            transitionNode.windowTitle = "Condition Check";
            windows.Add(transitionNode);

            return transitionNode;
        }


        public static void DrawNodeCurve(Rect start, Rect end, bool left, Color curveColor) {
            Vector3 startPos = new Vector3(
                (left) ? start.x + start.width : start.x,
                start.y + (start.height * 0.5f),
                0
                );

            Vector3 endPos = new Vector3(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f), 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;

            Color shadow = new Color(0, 0, 0, 0.06f);
            for (int i = 0; i < 3; i++) {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, shadow, null, (i + 1) * 0.5f);
            }


            Handles.DrawBezier(startPos, endPos, startTan, endTan, curveColor, null, 1);
        }

        public static void ClearWindowsFromList(List<BaseNode> nodes) {
            for (int i = 0; i < nodes.Count; i++) {
                if (windows.Contains(nodes[i])) {
                    windows.Remove(nodes[i]);
                }
            }
        }
        #endregion
    }
}