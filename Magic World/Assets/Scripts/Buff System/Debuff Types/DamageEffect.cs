using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuffSystem
{
    [CreateAssetMenu(menuName = "Status Effects/Debuffs/Damage Per Turn")]
    public class DamageEffect : StatusEffectObject
    {


        public override void OnTick(StatusEffect effect)
        {
            CharacterManager target = effect.target;
            target.TakeDamage(100);
        }
    }
}