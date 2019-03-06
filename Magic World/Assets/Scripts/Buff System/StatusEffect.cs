using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BuffSystem
{
    [System.Serializable]
    public class StatusEffect
    {
        public string effectName;
        public StatusEffectObject effectObject;
        public CharacterManager caster;
        public CharacterManager target;
        public int duration;
        public int potency;
        public int stacks = 1;

        public StatusEffect(CharacterManager newCaster, CharacterManager newTarget, StatusEffectObject newObject, int newPotency, int newDuration)
        {
            effectObject = newObject;
            caster = newCaster;
            target = newTarget;
            potency = newPotency;
            duration = newDuration;
        }


        public void AddEffect() {
            effectObject.ApplyEffect(this);
        }

        public void RemoveEffect() {
            effectObject.RemoveEffect(this);
        }

        public void RefreshDuration() {
            duration = effectObject.duration;
        }

        public void ExtendDuration(int extension) {
            duration += extension;
        }

        public void OverrideEffect(StatusEffect effect) {
            caster = effect.caster;
            duration = effect.duration;
            potency = effect.potency;
        }

        public void AddStack() {
            stacks++;
            //insert logic for stacking buffs
        }

        public void Tick() {
            duration--;
            effectObject.OnTick(this);

            if (duration == 0)
            {
                caster.RemoveStatus(this);
            }
        }

    }
}