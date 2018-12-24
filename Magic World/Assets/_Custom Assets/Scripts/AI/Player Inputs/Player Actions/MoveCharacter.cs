using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Move Character")]
    public class MoveCharacter : PlayerInputAction
    {
        public override void Execute(CharacterManager character)
        {
            if (character.caster2.currentAbility == null)
            {
                Vector3 direction = new Vector3(1, 0, 0) * World.inputs.GetAxis("Horizontal Left");
                direction += new Vector3(0, 0, 1) * World.inputs.GetAxis("Vertical Left");

                character.Move(direction);
                character.Rotate(direction);
            }
        }
    }
}