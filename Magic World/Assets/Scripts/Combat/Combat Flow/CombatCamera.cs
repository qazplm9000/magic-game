using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem
{
    public class CombatCamera : MonoBehaviour
    {

        public Camera mainCamera;
        private Combatant currentFighter;
        private Combatant fighterTarget;

        //unscaled distance vector from target
        public Vector3 offset;
        public Vector3 angleOffset;

        // Use this for initialization
        void Start()
        {
            if (mainCamera == null) {
                mainCamera = Camera.main;
            }

            mainCamera.transform.rotation = Quaternion.Euler(angleOffset.x, angleOffset.y, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (currentFighter != null) {
                mainCamera.transform.position = currentFighter.transform.position + offset;
                mainCamera.transform.rotation = Quaternion.Euler(angleOffset.x, angleOffset.y, 0);
            }
        }


    }
}