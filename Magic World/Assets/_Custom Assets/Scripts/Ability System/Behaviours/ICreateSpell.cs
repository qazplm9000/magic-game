using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability Behaviours/Create Spell")]
    public class ICreateSpell : AbilityBehaviour
    {

        public override void Init(CharacterManager character, GameObject go, BehaviourData data)
        {
            SpellBehaviour spell = World.spellPool.PullObjectBehaviour(data.behaviourObject);
            spell.target = character.target;

            GameObject newGo = spell.gameObject;
            newGo.transform.SetParent(character.transform);
            newGo.transform.localPosition = data.objectPosition;
            newGo.transform.localRotation = Quaternion.LookRotation(data.objectRotation);
            newGo.transform.SetParent(null);
            //Debug.Log("Instantiated");
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
