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
        private NavMeshAgent agent;
        [Header("How fast the character moves")]
        public float movementSpeed = 5;
        [Header("How fast the character turns in degrees/sec")]
        public float turnSpeed = 100f;
        public bool isJumping = false;

        //public bool canTurnInPlace = true;
        public Vector3 direction;

        private NavMeshPath path;
        private int pathIndex = 0;

        public float pathSensitivity = 0.1f;

        public void Start()
        {
            direction = transform.forward;
            rb = transform.GetComponent<Rigidbody>();
            agent = transform.GetComponent<NavMeshAgent>();
            agent.updatePosition = false;
            agent.updateRotation = false;
            
        }


        /// <summary>
        /// Used for playable characters
        /// All NPCs should only move forward while turning towards destination
        /// </summary>
        /// <param name="direction"></param>
        public void MoveInDirection(Camera camera, Vector3 direction)
        {
            Vector3 trueDirection = DirectionFromCamera(camera, direction);
            SetHorizontalVelocity(trueDirection * movementSpeed);

            if (trueDirection.magnitude != 0)
            {
                this.direction = trueDirection;
            }
        }


        /// <summary>
        /// Calculates a path to the destination
        /// Returns false if path not possible
        /// </summary>
        /// <param name="destination"></param>
        public bool SetDestination(Vector3 destination) {
            return agent.CalculatePath(destination, path);
        }


        /// <summary>
        /// Moves towards the destination
        /// Returns false when done
        /// </summary>
        /// <returns></returns>
        public bool MoveTowardsDestination() {
            bool hasReachedDestination = false;

            if (path != null && pathIndex < path.corners.Length)
            {
                Vector3 currentDestination = path.corners[pathIndex];

                GoToPoint(currentDestination);

                if ((currentDestination - transform.position).magnitude < pathSensitivity)
                {
                    pathIndex++;
                }
            }
            else {
                hasReachedDestination = true;
                path = null;
            }

            return !hasReachedDestination;
        }
        

        /// <summary>
        /// Goes to the point
        /// </summary>
        /// <param name="point"></param>
        private void GoToPoint(Vector3 point) {
            TurnTowards(point);
            MoveForward();
        }

        

        /// <summary>
        /// Moves the character forward
        /// </summary>
        public void MoveForward() {
            rb.velocity = transform.forward * movementSpeed;
        }


        /// <summary>
        /// Turns towards the direction
        /// </summary>
        /// <param name="direction"></param>
        public void TurnTowards(Vector3 direction) {
            Vector3 currentDirection = transform.forward;
            float angleBetween = Vector3.SignedAngle(currentDirection, direction, transform.up);

            float turnAngle = Mathf.Min(turnSpeed * Time.deltaTime, Mathf.Abs(angleBetween));
            turnAngle = angleBetween < 0 ? -turnAngle : turnAngle;

            transform.Rotate(transform.up, turnAngle);
        }

        /// <summary>
        /// Turn towards the direction the character was moving in
        /// </summary>
        /// <param name="character"></param>
        public void Rotate() {
            TurnTowards(direction);
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