using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Basic Combo", menuName = "Skill/Combo")]
    public class Combo : Skill
    {

        public List<ComboAttack> attacks = new List<ComboAttack>();
        private int currentAttack = -1;

        public override List<SkillAnimation> GetAnimations()
        {
            ComboAttack attack = GetNextAttack();
            return attack.animations;
        }

        public ComboAttack GetNextAttack()
        {
            currentAttack = (currentAttack + 1) % attacks.Count;
            return attacks[currentAttack];
        }

        public override float GetCastTime()
        {
            return attacks[currentAttack].castTime;
        }
    }
}