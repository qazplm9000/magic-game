using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [System.Serializable]
    public class SkillEffect
    {
        public SkillEffectTiming timing;
        public SkillEffectType effectType;
        [Tooltip("How much time to wait when Timing is set to \"After Delay\"")]
        public float delay;
        [Tooltip("Sets the speed upon activation")]
        public float speed = 5;

        [Tooltip("Skill object created when Type is set to \"Create Skill Object\"")]
        public SkillObjectData data;


        public SkillEffect(SkillEffect effect) {
            timing = effect.GetSkillEffectTiming();
            effectType = effect.GetSkillEffectType();
            delay = effect.GetDelay();
            data = effect.GetSkillObjectData();
        }

        public void ApplyEffect(SkillObject so, Combatant caster, Combatant target) {
            switch (effectType)
            {
                case SkillEffectType.DealDamage:
                    target.TakeDamage(5);
                    break;
                case SkillEffectType.HealHealth:
                    target.TakeDamage(-5);
                    break;
                case SkillEffectType.CreateSkillObject:
                    SkillObject obj = data.CreateSkillObject(caster, target, so);
                    break;
                case SkillEffectType.MoveForward:
                    
                    break;
                case SkillEffectType.TurnTowardsTarget:
                    break;
            }
        }


        public SkillEffect Copy() {
            SkillEffect result = new SkillEffect(this);

            return result;
        }


        public SkillEffectTiming GetSkillEffectTiming() { return timing; }
        public SkillEffectType GetSkillEffectType() { return effectType; }
        public float GetDelay() { return delay; }
        public SkillObjectData GetSkillObjectData() { return data; }
    }
}