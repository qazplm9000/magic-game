using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability Behaviours/Create")]
    public class ICreate : AbilityBehaviour
    {

        public override void Init(CharacterManager character, GameObject go, BehaviourData data)
        {
            GameObject newGo = World.spellPool.PullObject(data.behaviourObject);
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