using SkillSystem;
using UnityEngine;

namespace EffectSystem
{
    [CreateAssetMenu(menuName = "Skill/Effects/Damage Effect")]
    public class DamageEffect : Effect
    {
        public EffectTiming timing;
        public bool isHealing;
        public DamageFormula formula;

        public override void RunEffect(EffectData data)
        {
            int damage = 0;
            AttackData atkData = new AttackData(data.caster, data.target, data.potency, data.element, isHealing);
            damage = formula.CalculateDamage(atkData);
            data.target.TakeDamage(damage);
        }

        public override bool EffectIsFinished(EffectData data)
        {
            return true;
        }


        public override void UpdateDescription()
        {
            
        }
    }
}
