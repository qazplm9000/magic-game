using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    [CreateAssetMenu(menuName = "Modules/Movement/Rigid Body Movement")]
    public class RigidBodyMovement : CharacterMovement
    {
        public override void Move(CharacterManager character, Vector3 direction)
        {
            Vector3 trueDirection = character.DirectionFromCamera(direction);
            character.rb.velocity = trueDirection * character.movementSpeed;
        }
    }
}