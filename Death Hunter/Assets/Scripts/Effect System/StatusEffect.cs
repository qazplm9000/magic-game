using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    [CreateAssetMenu(menuName = "Skill/Status Effect", fileName = "Status Effect")]
    public class StatusEffect : ScriptableObject
    {
        public List<Effect> effects;

        public void RunStatus(float previousFrame, float currentFrame, Combatant user, Combatant target) {
            for (int i = 0; i < effects.Count; i++) {
                Effect effect = effects[i];
                effect.RunEffect(previousFrame, currentFrame, target);
            }
        }

        public bool StatusIsFinished(float previousFrame, float currentFrame) {
            bool result = true;

            for (int i = 0; i < effects.Count; i++) {
                result &= effects[i].EffectIsFinished(previousFrame, currentFrame);
            }

            return result;
        }
    }
}