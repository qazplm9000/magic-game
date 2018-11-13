using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability Behaviours/Create")]
    public class ICreate : AbilityBehaviour
    {

        public override void Init(AbilityCaster caster, GameObject go, BehaviourData data)
        {
            GameObject newGo = ObjectPool.pool.PullObject(data.behaviourObject);
            newGo.transform.SetParent(caster.transform);
            newGo.transform.localPosition = data.objectPosition;
            newGo.transform.localRotation = Quaternion.LookRotation(data.objectRotation);
            newGo.transform.SetParent(null);
        }


        public override bool Execute(AbilityCaster caster, GameObject go, BehaviourData data, float previousFrame, float nextFrame)
        {
            return false;
        }

        public override void End(AbilityCaster caster, GameObject go, BehaviourData data)
        {
            
        }

        public override void Interrupt(AbilityCaster caster, GameObject go, BehaviourData data)
        {
            
        }

    }
}