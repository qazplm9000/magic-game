using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Skill", menuName = "Skills/Skill")]
    public class Skill : ScriptableObject
    {

        new public string name = "";
        public string castAnimationName = "";
        public string animationName = "";
        public int power = 10;
        public float castTime = 1f;
        public int manaCost = 10;
        public SkillType skillType = SkillType.Melee;
        public Texture skillIcon;

        //effects playing while the spell is casting
        public List<SkillEffect> castEffects = new List<SkillEffect>();
        //effects playing upon the cast finishing
        public List<SkillEffect> effects = new List<SkillEffect>();
        public bool requireTarget = false;

        public virtual void StartCast(CombatController user, SkillCaster caster) {
            user.PlayAnimation(castAnimationName);

            foreach (SkillEffect effect in effects) {
                effect.CreateEffect(caster);
            }
        }

        public virtual void CastSkill(CombatController user, SkillCaster caster, CombatController target = null) {
            if (requireTarget && target == null) {
                return;
            }

            //play animation
            if (animationName != "") {
                user.PlayAnimation(animationName);
            }

            //instantiate all effects
            foreach (SkillEffect effect in effects) {
                effect.CreateEffect(caster);
            }
        }

        public void InstantiateEffects(SkillCaster user) {
            user.PlayAnimation(animationName);
        }


    }
    
}