using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BuffSystem
{
    public class StatusManager : MonoBehaviour
    {

        public List<StatusEffect> statusEffects;
        private Dictionary<StatusEffectObject, StatusEffect> _statusEffects;


        private void Start()
        {
            Init();
        }



        public void Tick() {
            for (int i = 0; i < statusEffects.Count; i++) {
                statusEffects[i].Tick();
            }
        }




        /// <summary>
        /// Applies the status effect to the character
        /// </summary>
        /// <param name="effect"></param>
        public void ApplyStatus(StatusEffect effect) {
            StatusEffectObject statusObj = effect.effectObject;

            if (!_statusEffects.ContainsKey(statusObj))
            {
                statusEffects.Add(effect);
                _statusEffects[statusObj] = effect;
                effect.AddEffect();
            }
            else {
                //Insert logic for if effect already present
                //might need logic for overriding buff if more powerful
                StatusEffect thisEffect = _statusEffects[statusObj];

                if (thisEffect.potency < effect.potency) {
                    effect.OverrideEffect(thisEffect);
                }
            }
        }


        /// <summary>
        /// Removes the effect
        /// Returns false if not found
        /// </summary>
        /// <param name="effect"></param>
        public bool RemoveStatus(StatusEffect effect) {
            StatusEffectObject statusObj = effect.effectObject;
            bool statusFound = false;

            if (_statusEffects.ContainsKey(statusObj)) {
                _statusEffects.Remove(statusObj);
                statusEffects.Remove(effect);
                effect.RemoveEffect();
                statusFound = true;
            }

            return statusFound;
        }

        /// <summary>
        /// Removes the effect at index
        /// </summary>
        /// <param name="effectNumber"></param>
        public bool RemoveStatus(int effectIndex) {
            bool statusFound = false;

            if (effectIndex < statusEffects.Count)
            {
                StatusEffectObject statusObj = statusEffects[effectIndex].effectObject;
                statusEffects.RemoveAt(effectIndex);
                _statusEffects.Remove(statusObj);
                statusFound = true;
            }

            return statusFound;
        }








        private void Init() {
            statusEffects = new List<StatusEffect>();
            _statusEffects = new Dictionary<StatusEffectObject, StatusEffect>();
        }

    }
}
