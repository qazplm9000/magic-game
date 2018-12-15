using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

namespace ControlSystem
{
    public abstract class InputHandler : MonoBehaviour
    {
        protected CharacterManager manager;
        public float trueSpeed;
        public bool isDead;
        public bool isInvincible;
        [HideInInspector]
        public static List<InputHandler> allCharacters = new List<InputHandler>();

        public float distanceFromPlayer;

        private Vector3 lastPosition;

        // Use this for initialization
        void Start()
        {
            Init();
        }


        //calculate a character's speed
        protected float CharacterSpeed()
        {
            float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
            lastPosition = CopyVector(transform.position);
            return speed;
        }

        /// <summary>
        /// Initializes all data used in every input handler
        /// </summary>
        protected void Init()
        {
            manager = transform.GetComponent<CharacterManager>();
            lastPosition = transform.position;
        }

        /// <summary>
        /// Copies a vector
        /// </summary>
        /// <param name="newVector"></param>
        /// <returns></returns>
        private Vector3 CopyVector(Vector3 newVector)
        {
            return new Vector3(newVector.x, newVector.y, newVector.z);
        }

        //determines whether a character is moving for animation purposes
        protected virtual bool IsCharacterMoving()
        {
            return trueSpeed != 0;
        }

    }
}