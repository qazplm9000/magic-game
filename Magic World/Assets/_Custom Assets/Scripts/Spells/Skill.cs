using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class Skill : ScriptableObject
    {

        new public string name;
        public string animationName;
        public int power = 10;
        public float castTime = 1f;
        public int manaCost = 10;
        public SkillType skillType = SkillType.Melee;
        public List<SkillEffect> effects = new List<SkillEffect>();


        public void InstantiateEffects(SkillUser user) {
            user.PlayAnimation(animationName);
        }


    }

    public enum SkillType {
        Melee,
        Magic,
        Healing,
        Support,
        Misc
    }
}