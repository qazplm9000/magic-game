using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(menuName="Dialogue Nodes/Choice Node")]
    public class ChoiceNode : DialogueNode
    {

        public List<string> options;

        //called when node is initialized
        public override void Start()
        {

        }

        //called every frame until all text has been updated
        public override bool Update()
        {
            return false;
        }

        //called when attempting to skip to end
        public override bool SkipText()
        {
            return false;
        }

        //called to choose the next option
        public override void Next()
        {

        }

    }
}