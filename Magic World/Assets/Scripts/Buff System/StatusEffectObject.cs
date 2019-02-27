using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuffSystem
{

    public abstract class StatusEffectObject : ScriptableObject
    {
        public string effectName;
        public Texture effectImage;
        public EffectTiming timing;
        public int duration;
        public bool stackable = false;
        public int maxStacks = 2;
        public List<StatusEffectObject> contradictingStatuses;
        public int potency;

        public StatusEffect CreateEffect(CharacterManager caster, CharacterManager target) {
            int newPotency = CalculatePotency(caster, target);
            StatusEffect newEffect = new StatusEffect(caster, target, this, newPotency, duration);

            return newEffect;
        }


        public virtual void ApplyEffect(StatusEffect effect) { }

        public virtual void RemoveEffect(StatusEffect effect) { }

        public virtual void OnTick(StatusEffect effect) { }

        public virtual void ForceRemoveEffect(StatusEffect effect) {
            RemoveEffect(effect);
        }

        public virtual int CalculatePotency(CharacterManager caster, CharacterManager target) {
            return potency;
        }
    }
}
