using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CombatSystem.SpellSystem
{
    public abstract class Skill : ScriptableObject
    {
        public string SpellName;
        public int potency;

        public abstract void CastSkill(SkillCaster caster, float previousFrame, float currentFrame);
        public abstract bool IsSkillFinished(SkillCaster caster, float previousFrame, float currentFrame);
        public abstract void OnCastStart(SkillCaster caster);
        public abstract void OnCastFinished(SkillCaster caster);
    }
}