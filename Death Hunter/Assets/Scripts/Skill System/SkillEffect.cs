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
    public class SkillEffect
    {
        public string description;
        public Effect effect;
        public float startTime;

        public void RunEffect(SkillCastData data) {
            if (data.AtTime(startTime))
            {
                data.target.ApplyEffect(effect);
            }
        }

        public void UpdateDescription()
        {
            effect.UpdateDescription();
            description = effect.description;
        }
    }
}