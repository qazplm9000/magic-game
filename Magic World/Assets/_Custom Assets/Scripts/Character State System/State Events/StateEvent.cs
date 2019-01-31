using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    public abstract class StateEvent : ScriptableObject
    {
        public abstract void Execute(CharacterManager character);
    }
}