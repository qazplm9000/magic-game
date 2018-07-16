using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName ="Effect", menuName = "Effects")]
    public class SkillEffect : ScriptableObject
    {

        public GameObject effect;
        public EffectBehaviour effectBehaviour;
        //public Transform castLocation;

        public void CreateEffect() {
            GameObject effectObject = ObjectPool.pool.PullObject(effect);
            EffectBehaviour objectBehaviour = ObjectPool.GetComponentFromObject<EffectBehaviour>(effectObject, effectBehaviour);
            

            //Add the component effectBehaviour 
            if (objectBehaviour != effectBehaviour) {
                effectObject.AddComponent(effectBehaviour.GetType());
            }
            
        }

    }
}