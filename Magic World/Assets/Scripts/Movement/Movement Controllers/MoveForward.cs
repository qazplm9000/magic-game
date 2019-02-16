using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovementSystem
{
    [CreateAssetMenu(menuName = "Modules/Movement/Move Forward")]
    public class MoveForward : CharacterMovement
    {
        public override void Move(MovementManager player, Vector3 direction)
        {
            //character.agent.velocity = character.transform.forward * character.movementSpeed;
        }
    }
}