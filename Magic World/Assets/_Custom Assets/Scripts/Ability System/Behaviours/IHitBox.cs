using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability Behaviours/Create Hitbox")]
    public class IHitBox : AbilityBehaviour
    {

        public override void Init(CharacterManager character, GameObject go, BehaviourData data)
        {
            Vector3 hitboxPos = character.transform.position + character.transform.forward * 0.2f;
            Hitbox hb = World.hitboxPool.PullObjectBehaviour(data.behaviourObject);
            hb.transform.position = hitboxPos;
            hb.CreateHitbox(character, data.runTime, data.objectPosition);
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
