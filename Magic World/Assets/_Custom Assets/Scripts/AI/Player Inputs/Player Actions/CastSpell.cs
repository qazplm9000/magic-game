using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Cast Spell")]
    public class CastSpell : PlayerInputAction
    {
        public override void Execute(CharacterManager character)
        {
            character.caster2.Cast(character.currentSpell);
        }
    }
}