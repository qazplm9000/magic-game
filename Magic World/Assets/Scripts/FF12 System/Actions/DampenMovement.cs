using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Dampen Movement")]
    public class DampenMovement : PlayerAction
    {
        [Range(0,1)]
        public float dampenModifier = 0;

        public override void Execute(CharacterManager character)
        {
            character.rb.velocity = character.rb.velocity * dampenModifier;
        }
    }
}