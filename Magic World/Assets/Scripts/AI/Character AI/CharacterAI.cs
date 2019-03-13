using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class CharacterAI : ScriptableObject
{
        
    public abstract void GetAction(CharacterManager character, CharacterController controller);

}
