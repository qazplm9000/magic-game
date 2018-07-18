using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName ="Effect", menuName = "Effects")]
    public class SkillEffect : ScriptableObject
    {

        public GameObject effect;
        public MonoBehaviour effectBehaviour;
        public CastLocation castLocation;

        public void CreateEffect(SkillCaster user) {
            Transform location = user.GetCastLocation(castLocation);
            GameObject effectObject = ObjectPool.pool.PullObject(effect, location);
            EffectBehaviour objectBehaviour = (EffectBehaviour)ObjectPool.GetComponentFromObject<MonoBehaviour>(effectObject, effectBehaviour);
            
            //Add the component effectBehaviour 
            if (objectBehaviour != effectBehaviour) {
                effectObject.AddComponent(effectBehaviour.GetType());
            }
        }

    }
}