using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability Behaviours/Animation")]
    public class IAnimation : AbilityBehaviour
    {

        public override void Init(AbilityCaster caster, GameObject go, BehaviourData data)
        {
            caster.manager.anim.CrossFade(data.animationName, 0.2f);
        }

        public override bool Execute(AbilityCaster caster, GameObject go, BehaviourData data, float previousFrame, float nextFrame)
        {
            return data.runTime > nextFrame;
        }

        public override void Interrupt(AbilityCaster caster, GameObject go, BehaviourData data)
        {
            return;
        }

        public override void End(AbilityCaster caster, GameObject go, BehaviourData data)
        {
            return;
        }

        
    }
}