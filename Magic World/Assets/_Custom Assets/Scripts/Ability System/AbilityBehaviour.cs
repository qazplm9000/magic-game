using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class AbilityBehaviour : ScriptableObject
    {

        public abstract void Init(CharacterManager character, GameObject go, BehaviourData data);

        //returns true while running
        public abstract bool Execute(CharacterManager character, GameObject go, BehaviourData data, float previousFrame, float nextFrame);

        public abstract void End(CharacterManager character, GameObject go, BehaviourData data);

        public abstract void Interrupt(CharacterManager character, GameObject go, BehaviourData data);
    }
}