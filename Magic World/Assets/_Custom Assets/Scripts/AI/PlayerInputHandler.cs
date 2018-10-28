using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;
//using ComboSystem;
using CombatSystem;

namespace ControlSystem
{
    public class PlayerInputHandler : InputHandler
    {
        float horizontal;
        float vertical;

        Camera mainCamera;

        // Use this for initialization
        void Start()
        {
            Init();
            mainCamera = Camera.main;
        }


        // Update is called once per frame
        void Update()
        {
            #region Movement and Animation variables
            //sets movement direction
            characterManager.direction = DirectionFromInput();
            //sets whether movement is locked based on animations
            characterManager.movementLocked = !CharacterCanMove();

            //sets variables for movement animation
            float speed = CharacterSpeed();
            characterManager.anim.SetFloat("Speed", characterManager.agent.velocity.magnitude);
            characterManager.anim.SetBool("isMoving", IsCharacterMoving());

            GetAxesValue();
            SetCharacterAxes();

            characterManager.anim.SetBool("lockOn", characterManager.isLockingOnTarget);
            #endregion

            //Debug.Log(characterManager.rb.velocity);
            //guard when not moving
            if (!characterManager.movementLocked && characterManager.agent.velocity.magnitude == 0 && !characterManager.isGuarding)
            {
                
                if (InputManager.manager.GetKeyDown("Dodge"))
                {
                    Debug.Log("Started guarding");
                    characterManager.combat.Guard();
                }
            }

            if (characterManager.isGuarding)
            {
                if (InputManager.manager.GetKeyUp("Dodge"))
                {
                    characterManager.combat.Unguard();
                }
            }

            //movement
            if (!characterManager.movementLocked)
            {
                Vector3 direction = characterManager.direction;
                //checks whether player is guarding or not when moving
                float moveSpeed = characterManager.movementSpeed;

                if (characterManager.isGuarding)
                {
                    moveSpeed *= characterManager.guardSpeedMultiplier;
                }

                if (characterManager.isLockingOnTarget)
                {
                    characterManager.movement.Strafe(characterManager.direction, moveSpeed * characterManager.strafeSpeedMultiplier);
                }
                else {
                    characterManager.movement.Move(characterManager.direction, moveSpeed);
                    //characterManager.movement.MoveWithoutNavMesh(characterManager.direction, moveSpeed);
                }
            }

            //Cast spells
            if (InputManager.manager.GetKeyDown("Cast"))
            {
                //characterManager.caster.Cast();
                characterManager.comboUser.UseCombo(false);
            }
            if (Input.GetKeyDown(KeyCode.K)) {
                characterManager.caster.ChangeIndex(1);
            }


            //Attack with left mouse or Square
            if (InputManager.manager.GetKeyDown("Attack"))
            {
                /*if (characterManager.target != null) {
                    characterManager.target.manager.stats.TakeDamage(5);
                }*/

                if (!characterManager.movementLocked)
                {
                    //characterManager.comboUser.UseCombo();
                    characterManager.caster.Cast();
                }
                else
                {
                    /*if (characterManager.bufferOpen)
                    {
                        characterManager.bufferedAction = Action.Attack;
                    }*/
                }
            }


            //locks and unlocks onto target
            if (InputManager.manager.GetKeyDown("Target")) {
                Debug.Log("Targetting nearest enemy");
                characterManager.combat.GetNearestEnemy();

                if (characterManager.target != null)
                {
                    characterManager.isLockingOnTarget = true;
                }
            }

            if (characterManager.target == null) {
                characterManager.isLockingOnTarget = false;
            }

            /*
            if (Input.GetKeyDown(KeyCode.H)) {
                animator.Play("Flinch", 2);
            }*/

        }


        public void FixedUpdate()
        {
            //dodge with Q
            if (InputManager.manager.GetKeyDown("Dodge") && characterManager.agent.velocity.magnitude != 0)
            {
                if (characterManager.combat.currentState == Action.Guard)
                {
                    characterManager.combat.Unguard();
                }

                if (!characterManager.movementLocked)
                {
                    StartCoroutine(characterManager.combat.Dodge(characterManager.direction));
                }
                else
                {
                    if (characterManager.combat.bufferOpen)
                    {
                        characterManager.combat.bufferedAction = Action.Dodge;
                    }
                }
            }


            //calls buffered action
            if (characterManager.combat.bufferedAction != Action.None && !characterManager.movementLocked)
            {
                switch (characterManager.combat.bufferedAction)
                {
                    case Action.Attack:
                        //controller.Rotate(DirectionFromInput());
                        //combos.Attack();
                        break;
                    case Action.Dodge:
                        characterManager.movement.Rotate(characterManager.direction);
                        StartCoroutine(characterManager.combat.Dodge(characterManager.direction));
                        break;
                    case Action.Skill:
                        break;
                }
                characterManager.combat.bufferedAction = Action.None;
            }
        }


        protected void GetAxesValue()
        {
            horizontal = InputManager.manager.GetAxis("Horizontal Left");
            vertical = InputManager.manager.GetAxis("Vertical Left");
        }

        //sets the horizontal and vertical axes
        protected void SetCharacterAxes()
        {
            characterManager.horizontal = horizontal;
            characterManager.vertical = vertical;
            characterManager.anim.SetFloat("Horizontal", horizontal);
            characterManager.anim.SetFloat("Vertical", vertical);
        }


        //determines whether a character is moving for animation purposes
        protected override bool IsCharacterMoving()
        {
            return base.characterManager.direction.magnitude != 0;
        }

        /// <summary>
        /// Returns whether the character can move from the animator
        /// </summary>
        /// <returns></returns>
        protected bool CharacterCanMove() {
            return characterManager.anim.GetBool("canMove");
        }

        /// <summary>
        /// Gets a direction from the input
        /// </summary>
        /// <returns></returns>
        protected Vector3 DirectionFromInput()
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward = new Vector3(cameraForward.x, 0, cameraForward.z);
            cameraForward /= cameraForward.magnitude;
            Vector3 cameraRight = mainCamera.transform.right;
            cameraRight = new Vector3(cameraRight.x, 0, cameraRight.z);
            cameraRight /= cameraRight.magnitude;
            Vector3 direction = cameraForward * InputManager.manager.GetAxis("Vertical Left");
            direction += cameraRight * InputManager.manager.GetAxis("Horizontal Left");



            if (direction.magnitude > 1)
            {
                direction /= direction.magnitude;
            }

            return direction;

        }

    }
}