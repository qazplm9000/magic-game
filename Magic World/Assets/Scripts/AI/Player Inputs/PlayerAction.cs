using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    public abstract class PlayerAction : ScriptableObject
    {
        public string description;
        public abstract void Execute(CharacterManager character);

    }
}