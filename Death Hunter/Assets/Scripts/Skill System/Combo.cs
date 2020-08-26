using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Basic Combo", menuName = "Skill/Combo")]
    public class Combo : Skill
    {

        public List<Skill> attacks = new List<Skill>();
        
        /*
        protected override void OnStart(SkillCastData data)
        {
            int currentCombo = data.caster.GetCurrentCombo() % attacks.Count;
            animations = attacks[currentCombo].animations;
            skillObjects = attacks[currentCombo].skillObjects;
            effects = attacks[currentCombo].effects;
            castTime = attacks[currentCombo].castTime;
            potency = attacks[currentCombo].potency;
        }

        protected override void OnRun(SkillCastData data)
        {
            
        }

        protected override void OnFinish(SkillCastData data)
        {
            data.caster.GetNextCombo();
        }*/
    }
}