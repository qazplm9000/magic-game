using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class NpcController : MonoBehaviour {

    public DialogueNode dialogue;
    public bool interactable = false;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }

    public virtual void Interact()
    {
        if (dialogue != null && interactable)
        {
            DialogueHandler.textBox.StartInteraction(dialogue);
        }
    }




    

}
