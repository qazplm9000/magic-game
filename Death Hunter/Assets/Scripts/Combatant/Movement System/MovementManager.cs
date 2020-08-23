using StateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.MovementSystem
{
    public class MovementManager : MonoBehaviour, IMovement
    {
        private Combatant character;
        private Rigidbody rb;

        public MovementType dimension = MovementType.Omnidirectional;
        public float speed = 5;
        public float rotationSpeed = 300;
        [Range(0,2)]
        public float dodgeTime = 1;
        public float dodgeInitialSpeed = 5;
        public float jumpForce = 10;

        public bool isDodging = false;

        private Vector3 previousPosition;
        private Vector3 currentPosition;
        private Quaternion previousRotation;
        private Quaternion currentRotation;
        public float currentSpeed = 0;
        public float currentRotationSpeed = 0;
        private float dodgeTimer = 0;



        private void Awake()
        {
            character = transform.GetComponent<Combatant>();
            rb = transform.GetComponent<Rigidbody>();
        }


        private void FixedUpdate()
        {
            if (character.GetFlag(Flag.character_is_dodging))
            {
                _Dodge();
            }
        }

        /// <summary>
        /// Moves a character in a certain direction
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Vector3 direction, float speedFraction = 1)
        {
            if (direction.magnitude != 0)
            {
                transform.position += direction * speed * speedFraction * Time.deltaTime;
            }

            if (character.GetFlag(Flag.character_is_grounded))
            {
                MoveToGround();
            }
        }

        /// <summary>
        /// Moves character to the ground
        /// </summary>
        public void MoveToGround()
        {
            RaycastHit hit;
            float maxDistance = 0.2f;
            Ray ray = new Ray(transform.position, -transform.up);

            if(Physics.Raycast(ray, out hit, maxDistance))
            {
                if(hit.collider.gameObject.layer == WorldManager.world.groundLayer)
                {
                    transform.position = hit.point;
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
                Vector3 flatDirection = new Vector3(direction.x, 0, direction.z);

                switch (dimension)
                {
                    case MovementType.Omnidirectional:
                        rotation = Quaternion.LookRotation(flatDirection, transform.up);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                        break;
                    case MovementType.ForwardsOnly:
                        rotation = Quaternion.LookRotation(flatDirection, transform.up);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                        break;
                    case MovementType.SideToSide:
                        break;
                }
            }
        }

        public void DisableGravity()
        {
            rb.useGravity = false;
        }

        public void EnableGravity()
        {
            rb.useGravity = true;
        }

        public void Jump(Vector3 direction)
        {
            Debug.Log(direction);
            rb.AddForce(transform.up * jumpForce);
            //rb.AddForce(direction * jumpForce);
        }


        public void Dodge(Vector3 direction)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, transform.up);
            transform.rotation = rotation;
            character.ChangeFlag(Flag.character_is_dodging, true);
            character.ChangeFlag(Flag.character_is_invincible, true);
        }
        

        private void _Dodge()
        {
            dodgeTimer += Time.fixedDeltaTime;
            transform.position += transform.forward * Time.fixedDeltaTime * dodgeInitialSpeed;

            if(dodgeTimer > dodgeTime)
            {
                ResetDodge();
            }
        }

        private void ResetDodge()
        {
            character.ChangeFlag(Flag.character_is_dodging, false);
            character.ChangeFlag(Flag.character_is_invincible, false);
            dodgeTimer = 0;
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

        public void LookAt(GameObject target)
        {
            Rotate(target.transform.position - transform.position);
        }
        

        public float GetSpeed()
        {
            return currentSpeed;
        }

        public void SetMaxSpeed(float newMaxSpeed)
        {
            speed = newMaxSpeed;
        }
    }
}