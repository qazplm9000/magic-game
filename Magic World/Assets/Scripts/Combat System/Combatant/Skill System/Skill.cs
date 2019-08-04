using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CombatSystem.SkillSystem
{
    [CreateAssetMenu(menuName = "Skills/Skill", fileName = "Skill")]
    public class Skill : ScriptableObject
    {
        public string SpellName;
        public int potency;
        public float castTime;
        public List<SkillEffect> effects;

        public void CastSkill(CombatManager combat, Combatant caster, float previousFrame, float currentFrame) {
            if (previousFrame == 0) {
                OnCastStart(combat, caster);
            }

            for (int i = 0; i < effects.Count; i++) {
                float startTime = effects[i].startTime;

                if (startTime >= previousFrame && startTime < currentFrame) {
                    effects[i].RunEffect(combat, caster);
                }
            }

            if (currentFrame > castTime) {
                OnCastFinished(combat, caster);
            }
        }

        public bool IsSkillFinished(CombatManager combat, Combatant caster, float previousFrame, float currentFrame) {
            return currentFrame > castTime;
        }

        private void OnCastStart(CombatManager combat, Combatant caster) {

        }

        private void OnCastFinished(CombatManager combat, Combatant caster) {

        }
    }
}