using BattleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Damage Formulas/Simple Damage Formula")]
    public class SimpleDamage : DamageFormula
    {
        public override int CalculateDamage(CharacterManager caster, CharacterManager target, BattleManager battleState, Ability ability)
        {
            int damage = caster.GetAttackStat();

            return damage;
        }
    }
}