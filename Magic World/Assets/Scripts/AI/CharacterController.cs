using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterController : MonoBehaviour
{
    public CharacterManager character;
    public bool isPlayer = false;

    


    public void Awake()
    {
        character = transform.GetComponent<CharacterManager>();
    }






    public void ReceiveInput(PlayerInput2 input) {
        Debug.Log("Received Input");
        if (character.allowedActions.ActionIsAllowed(input))
        {
            input.CallInput(character);
        }
    }

}
