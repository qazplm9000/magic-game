using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    [CreateAssetMenu(menuName = "Skill/Effect Collection", fileName = "Effect Collection")]
    public class EffectCollection : Effect, ISerializationCallbackReceiver
    {
        public List<BasicEffect> effects;

        

        public override void RunEffect(EffectData data)
        {
            for(int i = 0; i < effects.Count; i++)
            {
                effects[i].RunEffect(data);
            }
        }

        public override bool EffectIsFinished(EffectData data) {
            bool result = true;

            for (int i = 0; i < effects.Count; i++) {
                result &= effects[i].EffectIsFinished(data);
            }

            return result;
        }

        public override void UpdateDescription()
        {
            
        }

        public void OnBeforeSerialize()
        {
            duration = 0;

            for(int i = 0; i < effects.Count; i++)
            {
                if(effects[i] != null)
                {
                    duration = Mathf.Max(effects[i].duration, duration);
                }
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}