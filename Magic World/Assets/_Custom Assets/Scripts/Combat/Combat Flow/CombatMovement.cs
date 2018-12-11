using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CombatSystem
{
    public class CombatMovement : MonoBehaviour
    {

        private Camera mainCamera;
        private NavMeshAgent agent;

        public float speed = 5f;

        // Use this for initialization
        void Start()
        {
            mainCamera = Camera.main;
            agent = transform.GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            //get input
            Vector3 movement = Vector3.zero;
            movement += Input.GetAxis("Horizontal") * new Vector3(0, 0, 1);
            movement += Input.GetAxis("Vertical") * new Vector3(-1, 0, 0);
            
            //scale movement vector if over certain threshold
            //might still move faster diagonally for a short moment
            if (movement.magnitude > 1) {
                movement /= movement.magnitude;
            }

            movement = movement * Time.deltaTime * speed;

            agent.velocity = movement;
        }
    }
}