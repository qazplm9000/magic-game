using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "Dialogue Nodes/Text Node")]
    public class DialogueNode : ScriptableObject
    {

        public string text;
        public DialogueNode nextNode;

        //called when node is initialized
        public virtual void Start()
        {
            DialogueHandler.textBox.ClearText();
        }

        //called every frame until all text has been updated
        public virtual bool Update()
        {
            return DialogueHandler.textBox.UpdateText(text);
        }

        //called when attempting to skip to end
        public virtual bool SkipText()
        {
            DialogueHandler.textBox.SetText(text);
            return true;
        }

        //called to choose the next option
        public virtual void Next()
        {
            if (nextNode == null)
            {
                DialogueHandler.textBox.EndInteraction();
            }
            else
            {
                DialogueHandler.textBox.SetNode(nextNode);
            }
        }



    }
}