using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    public abstract class PlayerInputAction : ScriptableObject
    {
        public string description;
        public abstract void Execute(CharacterManager character);

    }
}