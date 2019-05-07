using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class CharacterAI : ScriptableObject
{
        
    public abstract ActionList GetActionList(CharacterManager character, CharacterController controller);

}
