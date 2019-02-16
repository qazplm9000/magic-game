using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Spells/Behaviours/Simple Follow")]
    public class SimpleFollowSpell : SpellController
    {
        public override void Execute(SpellBehaviour spell)
        {
            spell.transform.position += spell.transform.forward * spell.speed * Time.deltaTime;
            if (spell.target != null)
            {
                spell.transform.LookAt(spell.target.transform);
            }
        }
    }
}