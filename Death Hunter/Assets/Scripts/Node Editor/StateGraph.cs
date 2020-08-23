using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class StateGraph : EditorWindow
{
    private StateGraphView _graphView;

    [MenuItem("Graph/State Graph")]
    public static void OpenStateGraphWindow()
    {
        StateGraph window = GetWindow<StateGraph>();
        window.titleContent = new GUIContent("State Graph");
    }



    private void OnEnable()
    {
        _graphView = new StateGraphView
        {
            name = "State Graph"
        };

        rootVisualElement.Add(_graphView);
    }
}
