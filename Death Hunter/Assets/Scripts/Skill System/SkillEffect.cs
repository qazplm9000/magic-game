using CombatSystem;
using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    public enum SkillEffectTiming
    {
        OnCreate,
        AfterDelay,
        EveryFrame,
        OnCollision,
        OnDestroy
    }

    public enum SkillEffectType
    {
        DealDamage,
        HealHealth,
        CreateSkillObject
    }


    [System.Serializable]
    public class SkillEffect : ISerializationCallbackReceiver
    {
        public string description;
        public Effect effect;
        public float startTime;


        public void RunEffect(SkillCastData data) {
            if (data.AtTime(startTime))
            {
                EffectData ed = new EffectData(data.caster, data.target, data.skill.potency);
                data.target.ApplyEffect(effect, ed);
            }
        }

        public void UpdateDescription()
        {
        }
        
        public void OnAfterDeserialize()
        {
            if (Application.isEditor)
            {
                effect.UpdateDescription();
                description = effect.description;
            }
        }

        public void OnBeforeSerialize()
        {

        }
    }
}