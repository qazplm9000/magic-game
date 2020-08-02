using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.MovementSystem
{
    public class MovementManager : MonoBehaviour
    {

        public MovementType dimension = MovementType.Omnidirectional;
        public float speed = 5;
        public float rotationSpeed = 300;

        private Vector3 previousPosition;
        private Vector3 currentPosition;
        private Quaternion previousRotation;
        private Quaternion currentRotation;
        public float currentSpeed = 0;
        public float currentRotationSpeed = 0;


        private void Start()
        {
            
        }
        

        /// <summary>
        /// Moves a character in a certain direction
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Vector3 direction, float speedFraction = 1)
        {
            if (direction.magnitude != 0)
            {
                switch (dimension)
                {
                    case MovementType.Omnidirectional:
                        transform.position = transform.position + (direction * speed * speedFraction * Time.deltaTime);
                        break;
                    case MovementType.ForwardsOnly:
                        transform.position = transform.position + (transform.forward * speed * speedFraction * Time.deltaTime);
                        break;
                    case MovementType.SideToSide:
                        //Would have to figure out which side is closer and go in that direction
                        break;
                }
            }
        }

        /// <summary>
        /// Rotates the character to face a direction
        /// </summary>
        /// <param name="direction"></param>
        public void Rotate(Vector3 direction) {
            if (direction.magnitude != 0)
            {
                Quaternion rotation;

                switch (dimension)
                {
                    case MovementType.Omnidirectional:
                        rotation = Quaternion.LookRotation(direction, transform.up);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                        break;
                    case MovementType.ForwardsOnly:
                        rotation = Quaternion.LookRotation(direction, transform.up);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                        break;
                    case MovementType.SideToSide:
                        break;
                }
            }
        }



        public void LateUpdate()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            previousPosition = currentPosition;
            currentPosition = transform.position;
            previousRotation = currentRotation;
            currentRotation = transform.rotation;

            if (Time.deltaTime != 0)
            {
                currentSpeed = (previousPosition - currentPosition).magnitude / Time.deltaTime;
                currentRotationSpeed = Quaternion.Angle(previousRotation, currentRotation) / Time.deltaTime;
            }
        }

        private void InitPosition() {
            currentPosition = transform.position;
            previousPosition = transform.position;
            currentRotation = transform.rotation;
            previousRotation = transform.rotation;
            currentSpeed = 0;
            currentRotationSpeed = 0;
        }


        public float GetCurrentSpeed() { return currentSpeed; }
    }
}