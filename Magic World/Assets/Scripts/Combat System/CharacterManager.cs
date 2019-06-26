using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class CharacterManager : MonoBehaviour
    {

        public string characterName;

        private bool targetted = false;
        private float warnTime = 0;
        private CharacterManager targettedBy = null;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void TakeTurn() {

        }


        public void Idle() {

            
        }

        //Warns character when target is about to attack them
        public void Warn(CharacterManager target, float time) {
            targettedBy = target;
            warnTime = time;
            targetted = true;
            Debug.Log(name + " has been warned.");
        }

        //Called when an enemy is no longer targetting
        public void Unwarn() {
            targettedBy = null;
            warnTime = 0;
            targetted = false;
        }

    }
}