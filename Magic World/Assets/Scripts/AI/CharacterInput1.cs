using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{
    public abstract class CharacterInput : ScriptableObject
    {

        public abstract void Execute(CharacterManager character);
        

    }
}