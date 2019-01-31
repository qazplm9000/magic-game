using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    [CreateAssetMenu(menuName = "Modules/Movement/Move In Direction")]
    public class MoveFromCamera : CharacterMovement
    {

        public override void Move(CharacterManager character, Vector3 direction)
        {
            Vector3 trueDirection = character.DirectionFromCamera(direction);

            character.agent.velocity = trueDirection * character.movementSpeed;
        }

    }
}