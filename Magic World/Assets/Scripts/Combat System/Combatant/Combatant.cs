using CombatSystem.MovementSystem;
using CombatSystem.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewSkillSystem;
using CombatSystem.StatSystem;
using CombatSystem.CastLocationSystem;

namespace CombatSystem
{
    public class Combatant : MonoBehaviour
    {

        public string characterName;

        private bool targetted = false;
        private float warnTime = 0;
        private Combatant targettedBy = null;
        private bool isDead = false;

        public float nextTurnTime = 0f;
        public float turnSpeed = 5f;

        private CombatantController controller;
        private MovementManager movement;
        private CastManager caster;
        private StatManager stats;
        private CastLocationManager skeleton;
        

        // Start is called before the first frame update
        void Start()
        {
            controller = transform.GetComponent<CombatantController>();
            movement = transform.GetComponent<MovementManager>();
            caster = transform.GetComponent<CastManager>();
            stats = transform.GetComponent<StatManager>();
        }


        public void TakeTurn(CombatManager battleState) {
            if (controller != null) {
                controller.TakeTurn(battleState);
            }
        }


        public void Idle(CombatManager battleState) {
            
        }

        public void Cast(Skill skill) {
            caster.CastSkill(skill);
        }

        public List<Skill> GetSkillList() {
            return null;
        }

        public void Move(CombatManager battle, Vector3 direction)
        {
            movement.Move(direction);
            movement.Rotate(direction);
            Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        }


        //Warns character when target is about to attack them
        public void Warn(Combatant target, float time) {
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


        public void TakeDamage(int damage) {
            stats.AddStat(Stat.CurrentHealth, -damage);
            if (stats.GetStat(Stat.CurrentHealth) <= 0) {
                isDead = true;
                stats.SetBaseStat(Stat.CurrentHealth, -damage);
            }
        }

        public int GetStat(Stat stat) {
            return stats.GetStat(stat);
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


        public bool IsEnemy(Combatant target) {
            return true;
        }


        /// <summary>
        /// Combatant plays animation
        /// </summary>
        /// <param name="animationName"></param>
        public void PlayAnimation(string animationName) {
            Debug.Log("Character is playing animation: " + animationName);
        }

        /// <summary>
        /// Gets the gameobject of the specified body part
        /// </summary>
        /// <returns></returns>
        public GameObject GetBodyPart(CastLocation part) {
            return skeleton.GetBodyPart(part);
        }

        public Combatant GetTarget() {
            Combatant result = null;

            Combatant[] targets = GameObject.FindObjectsOfType<Combatant>();

            for (int i = 0; i < targets.Length; i++) {
                if (targets[i] != this) {
                    result = targets[i];
                    break;
                }
            }

            return result;
        }
    }
}