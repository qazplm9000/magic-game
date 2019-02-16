using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourEditor
{
    public class CommentNode : BaseNode
    {
        string comment = "Insert comment";

        public override void DrawCurves()
        {
            
        }

        public override void DrawWindow()
        {
            comment = GUILayout.TextArea(comment, 200);
        }
    }
}