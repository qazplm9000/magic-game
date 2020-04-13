using CombatSystem.MovementSystem;
using CombatSystem.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;
using CombatSystem.StatSystem;
using CombatSystem.CastLocationSystem;

namespace CombatSystem
{
    public class Combatant : MonoBehaviour
    {

        public string characterName;

        private bool targetted = false;
        private bool isDead = false;

        public float nextTurnTime = 0f;
        public float turnSpeed = 5f;

        private CombatantController controller;
        private MovementManager movement;
        private CastManager caster;
        private StatManager stats;
        private CastLocationManager skeleton;

        private Animator anim;
        

        // Start is called before the first frame update
        void Start()
        {
            controller = transform.GetComponent<CombatantController>();
            movement = transform.GetComponent<MovementManager>();
            caster = transform.GetComponent<CastManager>();
            stats = transform.GetComponent<StatManager>();

            anim = transform.GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (controller != null)
            {
                controller.ControlCharacter();
            }

            if (anim != null)
            {
                anim.SetFloat("Speed", movement.GetCurrentSpeed());
            }
        }

        

        public void Cast(Skill skill) {
            caster.CastSkill(skill);
        }

        public List<Skill> GetSkillList() {
            return null;
        }

        public void Move(Vector3 direction)
        {
            movement.Move(direction);
            movement.Rotate(direction);
            Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        }

        
        
        public void TakeDamage(int damage) {
            stats.AddStat(Stat.CurrentHealth, -damage);
            Debug.Log($"Character took {damage} damage");
        }

        public int GetStat(Stat stat) {
            return stats.GetStat(stat);
        }

        
        

        public bool IsEnemy(Combatant target) {
            return true;
        }


        /// <summary>
        /// Combatant plays animation
        /// </summary>
        /// <param name="animationName"></param>
        public void PlayAnimation(string animationName) {
            anim.Play(animationName);
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