using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class AbilityBehaviour : ScriptableObject
    {

        public abstract void Init(AbilityCaster caster, GameObject go, BehaviourData data);

        //returns true while running
        public abstract bool Execute(AbilityCaster caster, GameObject go, BehaviourData data, float previousFrame, float nextFrame);

        public abstract void End(AbilityCaster caster, GameObject go, BehaviourData data);

        public abstract void Interrupt(AbilityCaster caster, GameObject go, BehaviourData data);
    }
}