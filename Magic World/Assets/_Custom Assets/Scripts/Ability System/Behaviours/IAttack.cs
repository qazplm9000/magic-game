using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability Behaviours/Attack")]
    public class IAttack : AbilityBehaviour
    {
        
        public override void Init(CharacterManager character, GameObject go, BehaviourData data)
        {
            character.Attack();
        }

        public override bool Execute(CharacterManager character, GameObject go, BehaviourData data, float previousFrame, float nextFrame)
        {
            return true;
        }

        public override void End(CharacterManager character, GameObject go, BehaviourData data)
        {
            
        }

        public override void Interrupt(CharacterManager character, GameObject go, BehaviourData data)
        {
            
        }
    }
}