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

        public float nextTurnTime = 0f;
        public float turnSpeed = 5f;

        // Start is called before the first frame update
        void Start()
        {

        }


        public void TakeTurn(BattleManager battleState) {
            Move(battleState.camera);

        }


        public void Idle(BattleManager battleState) {

            
        }



        private void Move(Camera camera)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            Vector3 camForward = camera.transform.forward;
            camForward.y = 0;
            camForward.Normalize();
            Vector3 camRight = camera.transform.right;
            camRight.Normalize();

            camForward = camForward * vertical;
            camRight = camRight * horizontal;

            Vector3 direction = camForward + camRight;
            direction.Normalize();


            float rotation = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            Quaternion rot = new Quaternion();
            rot.eulerAngles = new Vector3(0, rotation, 0);

            transform.rotation = rot;
            transform.position += direction * Time.deltaTime * 10;

            Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
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


        /// <summary>
        /// Gets the time when character takes turn
        /// </summary>
        /// <param name="turnsInFuture"></param>
        /// <returns></returns>
        public float GetTurnTime(int turnsInFuture = 0) {
            float turnTime = 0;

            if (turnsInFuture >= 0)
            {
                turnTime = nextTurnTime + turnSpeed * turnsInFuture;
            }
            else{
                turnTime = nextTurnTime;
            }

            return turnTime;
        }

        /// <summary>
        /// Updates the turn time
        /// </summary>
        public void ProgressTurn() {
            nextTurnTime = nextTurnTime + turnSpeed;
        }


        public bool IsEnemy(CharacterManager target) {
            return true;
        }

    }
}