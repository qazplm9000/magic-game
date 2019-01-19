using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability Behaviours/Animation")]
    public class IAnimation : AbilityBehaviour
    {

        public override void Init(CharacterManager character, GameObject go, BehaviourData data)
        {
            character.anim.CrossFade(data.animationName, 0.2f);
        }

        public override bool Execute(CharacterManager character, GameObject go, BehaviourData data, float previousFrame, float nextFrame)
        {
            return data.runTime > nextFrame;
        }

        public override void Interrupt(CharacterManager character, GameObject go, BehaviourData data)
        {
            return;
        }

        public override void End(CharacterManager character, GameObject go, BehaviourData data)
        {
            return;
        }

        
    }
}