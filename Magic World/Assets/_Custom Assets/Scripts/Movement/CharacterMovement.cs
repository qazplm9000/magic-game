using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    public abstract class CharacterMovement : ScriptableObject
    {
        public string description;

        public abstract void Move(CharacterManager character, Vector3 direction);
    }
}