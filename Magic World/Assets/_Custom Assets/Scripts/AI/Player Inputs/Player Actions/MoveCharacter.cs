using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Move Character")]
    public class MoveCharacter : PlayerAction
    {
        public override void Execute(CharacterManager character)
        {
            Vector3 direction = new Vector3(1, 0, 0) * World.inputs.GetAxis("Horizontal Left");
            direction += new Vector3(0, 0, 1) * World.inputs.GetAxis("Vertical Left");

            Vector3 trueDirection = character.DirectionFromCamera(direction);
            character.movement.Move(character, trueDirection);

            if (direction.magnitude != 0)
            {
                character.turnDirection = trueDirection;
            }
        }
    }
}