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
        [Tooltip("How fast the character moves")]
        public float movementSpeed = 5;
        [Tooltip("How fast the character turns in degrees/sec")]
        public float turnSpeed = 100f;
        public bool isJumping = false;

        //public bool canTurnInPlace = true;
        public Vector3 turnDirection;

        private NavMeshPath path;
        private int pathIndex = 0;

        [Tooltip("How far away can the character be before considering a point reached")]
        public float pathSensitivity = 0.1f;
        [Tooltip("How many degrees can the character be turned before considered facing a target")]
        public float minAngle = 2f;

        public void Start()
        {
            turnDirection = transform.forward;
            rb = transform.GetComponent<Rigidbody>();
            agent = transform.GetComponent<NavMeshAgent>();
            agent.updatePosition = false;
            agent.updateRotation = false;
            
        }


        private void Update()
        {
            if (path != null) {
                for (int i = 0; i < path.corners.Length; i++) {
                    Debug.DrawRay(path.corners[i], Vector3.up * 3, Color.blue);
                    //Debug.Log(path.corners[i].x);
                }
                Debug.Log("Path has " + path.corners.Length + " points");
            }

            //Debug.DrawRay(transform.position, transform.forward * 3, Color.black);
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
        }


        /// <summary>
        /// Calculates a path to the destination
        /// Returns false if path not possible
        /// </summary>
        /// <param name="destination"></param>
        public bool SetDestination(Vector3 destination) {
            path = new NavMeshPath();
            RaycastHit characterHit;
            RaycastHit targetHit;

            Physics.Raycast(transform.position, -transform.up, out characterHit, 100);
            Physics.Raycast(destination, -transform.up, out targetHit, 100);

            return NavMesh.CalculatePath(characterHit.point, destination, NavMesh.AllAreas, path);
        }


        public bool HasPath() {
            return path != null;
        }


        /// <summary>
        /// Returns true if path index < length of corners in path
        /// </summary>
        /// <returns></returns>
        public bool HasReachedPathEnd() {
            return pathIndex >= path.corners.Length;
        }

        /// <summary>
        /// Moves towards the destination
        /// Returns false when done
        /// </summary>
        /// <returns></returns>
        public bool MoveTowardsDestination() {
            bool hasReachedDestination = false;

            if (HasPath() && !HasReachedPathEnd())
            {
                Vector3 currentDestination = path.corners[pathIndex];

                GoToPoint(currentDestination);

                Debug.DrawRay(currentDestination, Vector3.up * 3, Color.red);
                Debug.Log("Distance from corner:" + (currentDestination - transform.position).magnitude);
                //Debug.DrawLine(transform.position, currentDestination, Color.red);

                if ((currentDestination - transform.position).magnitude < pathSensitivity)
                {
                    pathIndex++;
                }
            }
            else {
                hasReachedDestination = true;
                path = null;
                pathIndex = 0;
            }

            return !hasReachedDestination;
        }
        

        /// <summary>
        /// Goes to the point
        /// </summary>
        /// <param name="point"></param>
        private void GoToPoint(Vector3 point)
        {
            TurnTowards(point);
            MoveForward();
        }

        

        /// <summary>
        /// Moves the character forward
        /// </summary>
        public void MoveForward() {
            rb.velocity = transform.forward * movementSpeed;
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
            float angleBetween = Vector3.SignedAngle(transform.forward, (position - transform.position), transform.up);

            float turnAngle = Mathf.Min(turnSpeed * Time.deltaTime, Mathf.Abs(angleBetween));

            if (turnAngle < minAngle) {
                turnAngle = 0;
            }

            turnAngle = angleBetween < 0 ? -turnAngle : turnAngle;

            transform.Rotate(transform.up, turnAngle);
        }

        /// <summary>
        /// Turn towards the direction the character was moving in
        /// </summary>
        /// <param name="character"></param>
        public void Rotate() {
            TurnTowards(turnDirection);
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