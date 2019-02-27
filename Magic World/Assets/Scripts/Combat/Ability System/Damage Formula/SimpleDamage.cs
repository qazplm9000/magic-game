using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Damage Formulas/Simple Damage Formula")]
    public class SimpleDamage : DamageFormula
    {
        public override int CalculateDamage(CharacterManager caster, CharacterManager target, Ability ability)
        {
            int damage = (caster.stats.strength - target.stats.defense / 2);

            return damage;
        }
    }
}