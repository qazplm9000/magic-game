using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;
using UnityEngine.AI;
using ComboSystem;

public class CharacterController : MonoBehaviour {

    CharacterMovement controller;
    CombatController combatController;
    SpellCaster spellCaster;
    new Camera camera;
    Animator animator;
    NavMeshAgent agent;
    CharacterCombos combos;

    public enum Action
    {
        Attack,
        Guard,
        Movement,
        None
    }

    float movementSpeed = 5f;
    Action lastAction = Action.None;
    bool movementLocked = false;
    Camera mainCamera;

    // Use this for initialization
    void Start () {
        controller = transform.GetComponentInChildren<CharacterMovement>();
        combatController = transform.GetComponent<CombatController>();
        spellCaster = transform.GetComponent<SpellCaster>();
        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
        combos = transform.GetComponent<CharacterCombos>();
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

        movementLocked = combatController.lockedMovement;

        

        //guard when not moving
        if (!movementLocked && agent.velocity.magnitude == 0 && !combatController.guarding)
        {
            if ((Input.GetButton("Dodge") || Input.GetKey(ControllerInputManager.controllerInput.dodgeButton)))
            {
                combatController.Guard();
            }
        }

        //unguard when you let go of button
        if(combatController.guarding && (Input.GetButtonUp("Dodge") || Input.GetKeyUp(ControllerInputManager.controllerInput.dodgeButton)))
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
        else
        {
            animator.SetFloat("Speed", 0);
        }

        //Cast spells
        if (Input.GetKeyDown(KeyCode.T)) {
            spellCaster.CastSpell(spellCaster.currentSpell);
        }


        //Attack with left mouse or Square
        if ((Input.GetButtonDown("Attack") || Input.GetKeyDown(ControllerInputManager.controllerInput.attackButton)) && !movementLocked)
        {
            combos.Attack();
        }
    }


    public void FixedUpdate()
    {
        //dodge with Q
        if ((Input.GetButtonDown("Dodge") || Input.GetKeyDown(ControllerInputManager.controllerInput.dodgeButton)) && !movementLocked && agent.velocity.magnitude != 0)
        {
            if (combatController.guarding)
            {
                combatController.Unguard();
            }

            StartCoroutine(combatController.Dodge(DirectionFromInput()));
        }
    }



    public Vector3 DirectionFromInput() {

        Vector3 direction = camera.transform.forward * Input.GetAxis("Vertical");
        direction += camera.transform.right * Input.GetAxis("Horizontal");

        if (direction.magnitude != 0) {
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



}
