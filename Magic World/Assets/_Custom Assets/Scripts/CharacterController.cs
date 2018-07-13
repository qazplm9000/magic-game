using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;
using UnityEngine.AI;
using ComboSystem;
using CombatSystem;

public class CharacterController : MonoBehaviour {

    CharacterMovement controller;
    CombatController combatController;
    SpellCaster spellCaster;
    new Camera camera;
    Animator animator;
    NavMeshAgent agent;
    CharacterCombos combos;

    float movementSpeed = 5f;
    bool movementLocked = false;
    Camera mainCamera;

    private Vector3 lastPosition;

    // Use this for initialization
    void Start () {
        controller = transform.GetComponentInChildren<CharacterMovement>();
        combatController = transform.GetComponent<CombatController>();
        spellCaster = transform.GetComponent<SpellCaster>();
        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
        combos = transform.GetComponent<CharacterCombos>();
        camera = Camera.main;

        lastPosition = CopyVector(transform.position);
	}
	
	// Update is called once per frame
	void Update () {

        movementLocked = combatController.lockedMovement;

        //sets variables for movement animation
        float speed = CharacterSpeed();
        animator.SetFloat("Speed", speed);
        animator.SetBool("Moving", IsCharacterMoving());

        //guard when not moving
        if (!movementLocked && agent.velocity.magnitude == 0 && !combatController.guarding)
        {
            if ((Input.GetButton("Dodge") || Input.GetKey(ControllerInputManager.input.dodgeButton)))
            {
                combatController.Guard();
            }
        }

        //unguard when you let go of button
        if(combatController.guarding && (Input.GetButtonUp("Dodge") || Input.GetKeyUp(ControllerInputManager.input.dodgeButton)))
        {
            combatController.Unguard();
        }

        //movement
        if (!movementLocked)
        {
            Vector3 direction = DirectionFromInput();
            //checks whether player is guarding or not when moving
            if (!combatController.guarding)
            {
                controller.Move(direction, movementSpeed);
            }
            else
            {
                controller.Move(direction, combatController.guardMoveSpeed);
            }
        }

        //Cast spells
        if (Input.GetKeyDown(KeyCode.T)) {
            spellCaster.CastSpell(spellCaster.currentSpell);
        }


        //Attack with left mouse or Square
        if ((Input.GetButtonDown("Attack") || Input.GetKeyDown(ControllerInputManager.input.attackButton)))
        {
            if (!movementLocked)
            {
                combos.Attack();
            }
            else {
                if (combatController.bufferOpen) {
                    combatController.bufferedAction = Action.Attack;
                }
            }
        }

        //doesn't work
        if (Input.GetKeyDown(KeyCode.Y)) {
            combatController.TakeDamage(10);
        }

        if (combatController.bufferedAction != Action.None && !combatController.lockedMovement) {
            switch (combatController.bufferedAction) {
                case Action.Attack:
                    //controller.Rotate(DirectionFromInput());
                    combos.Attack();
                    break;
                case Action.Dodge:
                    controller.Rotate(DirectionFromInput());
                    StartCoroutine(combatController.Dodge(DirectionFromInput()));
                    break;
                case Action.Skill:
                    break;
            }
            combatController.bufferedAction = Action.None;
        }


        if (Input.GetKeyDown(KeyCode.H)) {
            animator.Play("Flinch", 2);
        }

    }


    public void FixedUpdate()
    {
        //dodge with Q
        if ((Input.GetButtonDown("Dodge") || Input.GetKeyDown(ControllerInputManager.input.dodgeButton)) && agent.velocity.magnitude != 0)
        {
            if (combatController.currentState == Action.Guard)
            {
                combatController.Unguard();
            }

            if (!movementLocked)
            {
                StartCoroutine(combatController.Dodge(DirectionFromInput()));
            }
            else {
                if (combatController.bufferOpen)
                {
                    combatController.bufferedAction = Action.Dodge;
                }
            }
        }
    }



    public Vector3 DirectionFromInput() {

        Vector3 direction = camera.transform.forward * Input.GetAxis("Vertical");
        direction += camera.transform.right * Input.GetAxis("Horizontal");

        if (direction.magnitude > 1) {
            direction /= direction.magnitude;
        }

        return direction;

    }


    public void LockMovement() {
        movementLocked = true;
    }



    /*
    /// <summary>
    /// Locks movement for lockTime. Returns false if movement is already locked.
    /// </summary>
    /// <param name="lockTime"></param>
    /// <returns></returns>
    public bool TimedLockedMovement(float lockTime) {

        bool lockWorked = true;

        if (!movementLocked)
        {
            lockTimer = lockTime;
            movementLocked = true;
        }
        else {
            lockWorked = false;
        }

        return lockWorked;
    }
    
    */
    public void UnlockMovement() {
        movementLocked = false;
    }


    //determines whether a character is moving for animation purposes
    private bool IsCharacterMoving() {
        bool result = false;

        if (DirectionFromInput().magnitude != 0)
        {
            result = true;
        }

        return result;
    }

    //calculate a character's speed
    private float CharacterSpeed() {
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = CopyVector(transform.position);
        return speed;
    }

    private Vector3 CopyVector(Vector3 newVector) {
        return new Vector3(newVector.x, newVector.y, newVector.z);
    }

}
