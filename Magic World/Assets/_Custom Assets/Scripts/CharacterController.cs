using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;

public class CharacterController : MonoBehaviour {

    CharacterMovement controller;
    CombatController combatController;
    SpellCaster spellCaster;
    new Camera camera;
    Animator animator;

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
    private float lockTimer = 0f;
    Camera mainCamera;

    // Use this for initialization
    void Start () {
        controller = transform.GetComponentInChildren<CharacterMovement>();
        combatController = transform.GetComponent<CombatController>();
        spellCaster = transform.GetComponent<SpellCaster>();
        animator = transform.GetComponentInChildren<Animator>();
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        
        //lock and unlock movement with dodging
        if (combatController.dodging)
        {
            LockMovement();
        }
        if(movementLocked){
            if (!combatController.dodging)
            {
                UnlockMovement();
            }
        }

        //guard with E
        if (!movementLocked)
        {
            if (Input.GetKey(KeyCode.E))
            {
                combatController.Guard();
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                combatController.Unguard();
            }
        }

        //dodge with Q
        if (Input.GetKeyDown(KeyCode.Q) && !movementLocked) {
            combatController.Unguard();
            StartCoroutine(combatController.Dodge(DirectionFromInput()));
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

    public void UnlockMovement() {
        movementLocked = false;
    }


    

}
