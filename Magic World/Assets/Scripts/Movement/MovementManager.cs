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

        private Rigidbody rb;
        [Tooltip("How fast the character moves")]
        public float movementSpeed = 5;
        [Tooltip("How fast the character turns in degrees/sec")]
        public float turnSpeed = 100f;

        //public bool canTurnInPlace = true;
        public Vector3 turnDirection;

        

        public void Start()
        {
            turnDirection = transform.forward;
            rb = transform.GetComponent<Rigidbody>();
            
        }


        private void Update()
        {
            
        }


        /// <summary>
        /// Used for playable characters
        /// All NPCs should only move forward while turning towards destination
        /// </summary>
        /// <param name="direction"></param>
        public void MoveInDirection(Vector3 direction, float distance = 9999)
        {
            if (distance > Time.deltaTime * movementSpeed)
            {
                SetHorizontalVelocity(direction * movementSpeed);
            }
            else {
                TranslateInDirection(direction, distance);
                rb.velocity = Vector3.zero;
            }
        }


        

        /// <summary>
        /// Moves the character forward
        /// </summary>
        public void MoveForward() {
            SetHorizontalVelocity(transform.forward * movementSpeed);
        }


        public void FaceDirection(Vector3 direction) {
            if (direction.magnitude != 0) {
                turnDirection = direction;
            }
        }


        /// <summary>
        /// Turns towards the direction
        /// </summary>
        /// <param name="position"></param>
        public void TurnTowards(Vector3 position) {
            Vector3 fromAngle = transform.forward;
            fromAngle.y = 0;
            Vector3 toAngle = position - transform.position;
            toAngle.y = 0;
            Vector3 axis = Vector3.up;

            float angleBetween = Vector3.SignedAngle(fromAngle, toAngle, axis);

            float turnAngle = Mathf.Min(turnSpeed * Time.deltaTime, Mathf.Abs(angleBetween));
            

            turnAngle = angleBetween < 0 ? -turnAngle : turnAngle;

            transform.Rotate(transform.up, turnAngle);
        }
        

        /// <summary>
        /// Turn towards the direction the character was moving in
        /// </summary>
        /// <param name="character"></param>
        public void Rotate(float angle) {
            float turnOverTime = turnSpeed * Time.deltaTime;
            float finalTurn = 0;

            if (turnOverTime > Mathf.Abs(angle))
            {
                finalTurn = angle;
            }
            else {
                finalTurn = turnOverTime;
            }

            transform.Rotate(transform.up, finalTurn);
        }


        /// <summary>
        /// Adjusts the direction in relation to camera
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Vector3 DirectionFromCamera(Camera camera, Vector3 direction)
        {
            Vector3 camForward = camera.transform.forward;
            camForward = new Vector3(camForward.x, 0, camForward.z);
            camForward /= camForward.magnitude;
            Vector3 camRight = camera.transform.right;

            Vector3 result = camForward * direction.z;
            result += camRight * direction.x;

            if (result.magnitude > 1)
            {
                result /= result.magnitude;
            }

            return result;
        }
        

        public Vector3 GetHorizontalVelocity() {
            Vector3 currentVelocity = rb.velocity;

            return new Vector3(currentVelocity.x, 0, currentVelocity.z);
        }

        private void TranslateInDirection(Vector3 direction, float distance) {
            rb.MovePosition(transform.position + direction / direction.magnitude * distance);
        }

        /// <summary>
        /// Sets the X and Z values for the velocity
        /// </summary>
        /// <param name="direction"></param>
        public void SetHorizontalVelocity(Vector3 direction) {
            rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.z);
        }

        public float GetMaxMovementSpeed() {
            return movementSpeed;
        }

        public void SetMaxMovementSpeed(float speed) {
            movementSpeed = speed;
        }

        public float GetCurrentSpeed() {
            return rb.velocity.magnitude;
        }


        public void EnableGravity(bool useGravity = true) {
            rb.useGravity = useGravity;
        }

    }
}