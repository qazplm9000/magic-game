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
            character.rb.velocity = direction * character.movementSpeed;
        }
    }
}