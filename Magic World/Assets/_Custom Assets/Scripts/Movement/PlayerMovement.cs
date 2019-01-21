using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using InputSystem;

//Script used for containing all movement functions

namespace MovementSystem
{
    public class PlayerMovement : CharacterMovement
    {

        private Camera mainCamera;

        [Range(0, 1)]
        public float smoothing = 0.7f;

        // Use this for initialization
        void Awake()
        {
            mainCamera = Camera.main;
        }


        /// <summary>
        /// Smoothly rotate character
        /// </summary>
        /// <param name="newDirection"></param>
        public void SmoothRotate(CharacterManager character, Vector3 direction, float turnSpeed)
        {
            //get direction based on direction and camera direction
            //Vector3 cameraDirection = mainCamera.transform.forward;
            //cameraDirection.y = 0;
            Vector3 newDirection = new Vector3(direction.x, 0, direction.z);

            if (newDirection.magnitude != 0)
            {
                float angle = Vector3.SignedAngle(character.transform.forward, newDirection, Vector3.up);
                angle = Mathf.LerpAngle(0, angle, turnSpeed);
                character.transform.Rotate(character.transform.up, angle);
            }
        }

        //Smoothly rotate character
        public void SmoothRotate(CharacterManager character, Vector3 direction)
        {
            SmoothRotate(character, direction, smoothing);
        }

        /*
        /// <summary>
        /// Rotate without smoothing
        /// </summary>
        /// <param name="direction"></param>
        public override void Rotate(CharacterManager character, Vector3 direction)
        {
            if (direction.magnitude != 0)
            {
                float angle = Vector3.SignedAngle(character.transform.forward, direction, Vector3.up);
                character.transform.Rotate(character.transform.up, angle);
            }
        }
        */

        //take a direction and move towards it
        public override void Move(CharacterManager character, Vector3 direction)
        {
            //direction with Y movement removed
            //Vector3 trueDirection = new Vector3(direction.x, 0, direction.z);

            character.agent.velocity = direction * character.movementSpeed;
            //SmoothRotate(trueDirection);

        }


        public Vector3 DirectionFromCamera(Vector3 direction)
        {
            Vector3 camForward = mainCamera.transform.forward;
            camForward = new Vector3(camForward.x, 0, camForward.z);
            camForward /= camForward.magnitude;
            Vector3 camRight = mainCamera.transform.right;

            Vector3 result = camForward * direction.z;
            result += camRight * direction.x;

            if (result.magnitude > 1)
            {
                result /= result.magnitude;
            }

            return result;
        }
        /*

        /// <summary>
        /// Halt all movement
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="movementSpeed"></param>
        public void HaltMovement(CharacterManager character)
        {
            character.agent.velocity = Vector3.zero;
        }


        public void MoveWithoutNavMesh(CharacterManager character, Vector3 direction, float movementSpeed)
        {
            //moves the character in the direction
            //transform.position += direction * movementPercent * movementSpeed * Time.deltaTime;
            Vector3 trueDirection = new Vector3(direction.x, 0, direction.z);
            character.rb.velocity = trueDirection * movementSpeed +
                                                (character.rb.velocity.y * Vector3.down +
                                                Physics.gravity * Time.deltaTime);
        }

    */
    }
}