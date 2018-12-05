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
            manager.direction = DirectionFromInput();
            //sets whether movement is locked based on animations
            manager.movementLocked = !CharacterCanMove();

            //sets variables for movement animation
            float speed = CharacterSpeed();
            manager.anim.SetFloat("Speed", manager.agent.velocity.magnitude);
            manager.anim.SetBool("isMoving", IsCharacterMoving());

            GetAxesValue();
            SetCharacterAxes();

            manager.anim.SetBool("lockOn", manager.isLockingOnTarget);
            #endregion

            //Debug.Log(characterManager.rb.velocity);
            //guard when not moving
            if (!manager.movementLocked && manager.agent.velocity.magnitude == 0 && !manager.isGuarding)
            {
                
                if (InputManager.manager.GetKeyDown("Dodge"))
                {
                    Debug.Log("Started guarding");
                    manager.combat.Guard();
                }
            }

            if (manager.isGuarding)
            {
                if (InputManager.manager.GetKeyUp("Dodge"))
                {
                    manager.combat.Unguard();
                }
            }

            //movement
            if (!manager.movementLocked)
            {
                Vector3 direction = manager.direction;
                //checks whether player is guarding or not when moving
                float moveSpeed = manager.movementSpeed;

                if (manager.isGuarding)
                {
                    moveSpeed *= manager.guardSpeedMultiplier;
                }

                if (manager.isLockingOnTarget)
                {
                    manager.movement.Strafe(manager.direction, moveSpeed * manager.strafeSpeedMultiplier);
                }
                else {
                    manager.movement.Move(manager.direction, moveSpeed);
                    //characterManager.movement.MoveWithoutNavMesh(characterManager.direction, moveSpeed);
                }
            }



            if (!manager.movementLocked)
            {


                //Cast spells
                if (InputManager.manager.GetKeyDown("Cast"))
                {
                    //characterManager.caster.CastSpell();
                    manager.agent.velocity = Vector3.zero;
                    manager.combos.UseSpell();
                    Debug.Log("Casting spell");
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //characterManager.caster.ChangeIndex(1);
                    manager.caster.IncrementIndex();
                }

                if (Input.GetKeyDown(KeyCode.Alpha4)) {
                    manager.caster2.Cast(manager.caster2.abilityList[0]);
                }


                //Attack with left mouse or Square
                if (InputManager.manager.GetKeyDown("Attack"))
                {
                    manager.combos.UseCombo();

                    if (!manager.movementLocked)
                    {
                        //characterManager.comboUser.UseCombo();
                        //characterManager.caster.Cast();
                    }
                    else
                    {
                        /*if (characterManager.bufferOpen)
                        {
                            characterManager.bufferedAction = Action.Attack;
                        }*/
                    }
                }
            }

            if (manager.target != null) {
                manager.movement.SmoothRotate(manager.target.transform.position - transform.position, 0.1f);
            }


            //locks and unlocks onto target
            if (InputManager.manager.GetKeyDown("Target")) {
                Debug.Log("Targetting nearest enemy");
                //characterManager.combat.GetNearestEnemy();
                if (manager.target == null)
                {
                    manager.target = manager.targetter.GetNearestTarget();
                    manager.isLockingOnTarget = true;
                }
                else {
                    manager.target = manager.targetter.SwitchTarget();
                }
            }

            if (manager.target == null) {
                manager.isLockingOnTarget = false;
            }

            /*
            if (Input.GetKeyDown(KeyCode.H)) {
                animator.Play("Flinch", 2);
            }*/

        }


        public void FixedUpdate()
        {
            //dodge with Q
            if (InputManager.manager.GetKeyDown("Dodge") && manager.agent.velocity.magnitude != 0)
            {
                if (manager.combat.currentState == Action.Guard)
                {
                    manager.combat.Unguard();
                }

                if (!manager.movementLocked)
                {
                    StartCoroutine(manager.combat.Dodge(manager.direction));
                }
                else
                {
                    if (manager.combat.bufferOpen)
                    {
                        manager.combat.bufferedAction = Action.Dodge;
                    }
                }
            }


            //calls buffered action
            if (manager.combat.bufferedAction != Action.None && !manager.movementLocked)
            {
                switch (manager.combat.bufferedAction)
                {
                    case Action.Attack:
                        //controller.Rotate(DirectionFromInput());
                        //combos.Attack();
                        break;
                    case Action.Dodge:
                        manager.movement.Rotate(manager.direction);
                        StartCoroutine(manager.combat.Dodge(manager.direction));
                        break;
                    case Action.Skill:
                        break;
                }
                manager.combat.bufferedAction = Action.None;
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
            manager.horizontal = horizontal;
            manager.vertical = vertical;
            manager.anim.SetFloat("Horizontal", horizontal);
            manager.anim.SetFloat("Vertical", vertical);
        }


        //determines whether a character is moving for animation purposes
        protected override bool IsCharacterMoving()
        {
            return base.manager.direction.magnitude != 0;
        }

        /// <summary>
        /// Returns whether the character can move from the animator
        /// </summary>
        /// <returns></returns>
        protected bool CharacterCanMove() {
            return manager.anim.GetBool("canMove");
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