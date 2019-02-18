using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{
    public abstract class CharacterAI : ScriptableObject
    {
        
        

        //player maybe runs every frame?
        //enemy would have to run less often, maybe every state change it decides on another action
        //  Might need to add some way to delay an input
        //  probably need some state system to determine chances of each action

        //public PlayerAction GetAction(CharacterManager character, AllowedActions allowedActions)

    }
}