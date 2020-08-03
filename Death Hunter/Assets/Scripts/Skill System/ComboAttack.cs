using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Combo Attack", menuName = "Skill/Combo Attack")]
    public class ComboAttack : ScriptableObject
    {
        public List<SkillAnimation> animations = new List<SkillAnimation>();
        public List<SkillEffect> effects = new List<SkillEffect>();
        public float castTime;
    }
}