using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Cast Spell")]
    public class CastSpell : PlayerAction
    {
        public override void Execute(CharacterManager character)
        {
            character.RunAbility();
        }
    }
}