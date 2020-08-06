using CombatSystem.MovementSystem;
using CombatSystem.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;
using CombatSystem.StatSystem;
using CombatSystem.CastLocationSystem;
using EffectSystem;
using StateSystem;
using TargettingSystem;

namespace CombatSystem
{
    public class Combatant : MonoBehaviour
    {

        public string characterName;
        public int currentCombo = 0;
        public List<string> enemyTags = new List<string>();

        private CombatantController controller;
        private MovementManager movement;
        private CastManager caster;
        private StatManager stats;
        private CastLocationManager skeleton;
        private StateManager state;
        private AudioSource audio;
        private TargetManager targetter;

        private Animator anim;


        // Start is called before the first frame update
        void Start()
        {
            controller = transform.GetComponent<CombatantController>();
            movement = transform.GetComponent<MovementManager>();
            caster = transform.GetComponent<CastManager>();
            stats = transform.GetComponent<StatManager>();
            state = transform.GetComponent<StateManager>();
            audio = transform.GetComponent<AudioSource>();
            targetter = transform.GetComponent<TargetManager>();

            anim = transform.GetComponentInChildren<Animator>();
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.K))
            {
                PlayAnimation("New State");
                anim.SetBool("isGrounded", false);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                anim.SetBool("isGrounded", true);
            }

            if (controller != null)
            {
                controller.ControlCharacter();
            }

            if (anim != null)
            {
                anim.SetFloat("Speed", movement.GetCurrentSpeed());
            }

            if(GetStat(Stat.CurrentHealth) <= 0)
            {
                gameObject.SetActive(false);
            }

            if(HasTarget())
            {
                movement.LookAt(GetTarget().gameObject);
                anim.SetBool("isTargetting", true);
            }
            else
            {
                anim.SetBool("isTargettng", false);
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
            if (state.GetFlag(Flag.character_can_move))
            {
                movement.Move(direction);

                if (!HasTarget())
                {
                    movement.Rotate(direction);
                }

                Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
            }
        }



        public void TakeDamage(int damage) {
            stats.AddStat(Stat.CurrentHealth, -damage);
            WorldManager.world.ShowDamageValue(this, damage);
            Debug.Log($"Character took {damage} damage");
        }

        public int GetStat(Stat stat) {
            return stats.GetStat(stat);
        }


        /// <summary>
        /// Combatant plays animation
        /// </summary>
        /// <param name="animationName"></param>
        public void PlayAnimation(string animationName, float animCrossFade = 0) {
            anim.CrossFade(animationName, animCrossFade);
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
            return targetter.currentTarget;
        }


        public void ApplyEffect(Effect effect) {
            transform.GetComponent<EffectManager>().AddEffect(effect);
        }

        public void ChangeFlag(Flag flag, bool flagValue)
        {
            state.ChangeFlag(flag, flagValue);
        }


        public void PlayAudio(AudioClip clip)
        {
            audio.PlayOneShot(clip);
        }

        public int GetNextCombo()
        {
            currentCombo++;
            return currentCombo;
        }

        public int GetCurrentCombo()
        {
            return currentCombo;
        }

        public void ResetCurrentCombo()
        {
            currentCombo = 0;
        }

        public bool IsEnemy(Combatant target)
        {
            string tag = target.tag;

            return enemyTags.Contains(tag);
        }

        public bool IsAlly(Combatant target)
        {
            return tag == target.tag;
        }

        public bool IsDead()
        {
            return GetStat(Stat.CurrentHealth) <= 0;
        }

        public void SetAnimationBool(string boolName, bool value)
        {
            anim.SetBool(boolName, value);
        }

        public bool HasTarget()
        {
            return GetTarget() != null;
        }
    }
}