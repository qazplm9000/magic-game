using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    [CreateAssetMenu(menuName = "Modules/Movement/Rigid Body Movement")]
    public class RigidBodyMovement : CharacterMovement
    {
        public override void Move(MovementManager player, Vector3 direction)
        {
            player.rb.velocity = direction * player.movementSpeed;
            //Debug.Log("Velocity: " + player.rb.velocity.magnitude);
        }
    }
}