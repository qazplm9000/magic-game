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
using WeaponSystem;

namespace CombatSystem
{
    public class Combatant : MonoBehaviour, IDamageable
    {

        public string characterName;
        public int currentCombo = 0;
        public List<string> enemyTags = new List<string>();

        private CombatantController controller;
        private MovementManager movement;
        private Caster caster;
        private StatManager stats;
        private CastLocationManager skeleton;
        private StateManager state;
        private AudioSource audio;
        private EffectManager effects;
        private ITargetter targetter;
        public WeaponObject weapon;
        private Rigidbody rb;
        private CharacterSwapper swapper;

        public AudioClip runningClip;

        public float despawnTime = 5;
        private Timer despawnTimer;

        private Animator anim;


        // Start is called before the first frame update
        void Start()
        {
            controller = transform.GetComponent<CombatantController>();
            movement = transform.GetComponent<MovementManager>();
            caster = transform.GetComponent<Caster>();
            stats = transform.GetComponent<StatManager>();
            state = transform.GetComponent<StateManager>();
            audio = transform.GetComponent<AudioSource>();
            targetter = transform.GetComponent<ITargetter>();
            effects = transform.GetComponent<EffectManager>();
            rb = transform.GetComponent<Rigidbody>();
            swapper = transform.GetComponent<CharacterSwapper>();

            despawnTimer = new Timer();

            anim = transform.GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (controller != null && !stats.IsDead())
            {
                controller.ControlCharacter();
            }

            if (anim != null)
            {
                //anim.SetFloat("Speed", movement.GetSpeed());
            }

            if(HasTarget())
            {
                movement.LookAt(GetCurrentTarget().gameObject);
                //anim.SetBool("isTargetting", true);
            }
            else
            {
                //anim.SetBool("isTargetting", false);
            }

            if(movement.currentSpeed > 2 && !audio.isPlaying)
            {
                audio.clip = runningClip;
                audio.Play();
            }else if(movement.currentSpeed <= 2)
            {
                audio.Stop();
            }

            if (stats.IsDead())
            {
                anim.SetBool("isDead", true);
                despawnTimer.Tick();
                Debug.Log("Despawning enemy");
                if (despawnTimer.AtTime(despawnTime))
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void Jump(Vector3 direction)
        {
            if (GetFlag(Flag.character_can_jump))
            {
                movement.Jump(direction);
                ChangeFlag(Flag.character_jumped, true);
                //ChangeFlag(Flag.character_is_grounded, false);
            }
        }

        public void Guard()
        {
            if (GetFlag(Flag.character_can_guard))
            {
                ChangeFlag(Flag.character_is_guarding, true);
            }
        }

        public void Unguard()
        {
            ChangeFlag(Flag.character_is_guarding, false);
        }


        /* Weapon Hitbox */

        public void ActivateWeaponHitbox(List<Effect> effects, int potency)
        {
            weapon.ActivateCollider(effects, potency);
        }

        public void DeactivateWeaponHitbox()
        {
            weapon.DeactivateCollider();
        }





        public void Cast(Skill skill) {
            if (GetFlag(Flag.character_can_cast))
            {
                //caster.CastSkill(skill, GetCurrentTarget());
            }
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

        public void MoveToGround()
        {
            movement.MoveToGround();
        }

        public void Dodge(Vector3 direction)
        {
            if (GetFlag(Flag.character_can_dodge))
            {
                movement.Dodge(direction);
            }
        }


        public void TakeDamage(int damage) {
            if (!stats.IsDead())
            {
                stats.TakeDamage(damage);
                //WorldManager.world.ShowDamageValue(this, damage);
                Debug.Log($"Character took {damage} damage");
            }
        }

        public void HealHealth(int healing)
        {
            stats.HealHealth(healing);
        }

        public int GetStat(StatType stat) {
            return stats.GetStat(stat);
        }

        public bool IsDead()
        {
            return GetStat(StatType.CurrentHealth) <= 0;
        }

        public StatSnapshot CreateStatSnapshot()
        {
            return stats.CreateSnapshot();
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

        public Combatant GetCurrentTarget() {
            Combatant result = null;
            if (targetter != null)
            {
                result = targetter.GetCurrentTarget();
            }
            return result;
        }

        public Combatant GetNearestTarget()
        {
            return targetter.TargetEnemy();
        }

        public Combatant GetTargetToRight()
        {
            return targetter.TargetEnemy(true);
        }

        public Combatant GetTargetToLeft()
        {
            return targetter.TargetEnemy(false);
        }

        public void Untarget()
        {
            targetter.Untarget();
        }





        public void ChangeCharacter()
        {
            swapper.ChangeCharacter();
        }




        public void ApplyEffect(Effect effect, EffectData data) {
            effects.AddEffect(effect, data);
        }

        public void ChangeFlag(Flag flag, bool flagValue)
        {
            state.ChangeFlag(flag, flagValue);
        }

        public bool GetFlag(Flag flag)
        {
            return state.GetFlag(flag);
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

        public void SetAnimationBool(string boolName, bool value)
        {
            anim.SetBool(boolName, value);
        }

        public void SetAnimationFloat(string floatName, float value)
        {
            anim.SetFloat(floatName, value);
        }
        
        public void SetAnimationInt(string intName, int value)
        {
            anim.SetInteger(intName, value);
        }

        public bool HasTarget()
        {
            return GetCurrentTarget() != null;
        }

        public void EnableGravity()
        {
            rb.useGravity = true;
        }

        public void DisableGravity()
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.useGravity = false;
        }



        public void ResetState()
        {
            state.ResetState();
        }
    }
}