using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using InputSystem;

//Script used for containing all movement functions

namespace MovementSystem
{
    public class MovementManager : MonoBehaviour
    {
        public CharacterMovement movementController;
        public CharacterRotation rotationController;

        public Rigidbody rb;
        public float movementSpeed = 5;

        public Vector3 direction;

        public void Start()
        {
            direction = transform.forward;
            rb = transform.GetComponent<Rigidbody>();
        }

        public void Move(Vector3 direction)
        {
            movementController.Move(this, direction);

            if (direction.magnitude != 0)
            {
                this.direction = direction;
            }
        }

        /// <summary>
        /// Turn towards the direction the character is moving in
        /// </summary>
        /// <param name="character"></param>
        public void Rotate() {
            rotationController.Rotate(this, direction);
        }

        /// <summary>
        /// Turn towards the direction
        /// </summary>
        /// <param name="character"></param>
        /// <param name="direction"></param>
        public void Rotate(Vector3 direction) {
            rotationController.Rotate(this, direction);
        }
    }
}