using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AbilitySystem;
using InputSystem;
using CombatSystem;
using StatSystem;
using CharacterStateSystem;


public class CharacterManager : MonoBehaviour {


    [HideInInspector]
    public float vertical;
    [HideInInspector]
    public float horizontal;



    /*
    //Modules for character
    public SkillAnimator animator; //used for any skill animations
    public CombatManager combat;
    public TargetManager targetter;
    public CharacterStats stats;
    public MovementManager movement;

    //used in combat
    public CharacterController battleController;    
    //used everywhere else
    //enemies have AI controllers for battle and overworld
    //other party members following (if there are any) would have AI following scripts
    public CharacterController overworldController; 

     
     */



    // [HideInInspector]
    //public CharacterController controller;
    public CombatController combat;
    public PlayerTargetter targetter;
    public CharacterMovement movement;
    public CharacterStats stats;
    public AbilityCaster caster2;
    public SimpleCombo combos;

    //used for allowing different character models
    public GameObject activeModel;

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Vector3 direction;
    public Vector3 turnDirection;
    //public CharacterState state;

    [Header("Movement")]
    public float movementSpeed = 5;
    public bool movementLocked = false;
    public float trueSpeed = 0;
    

    public TargetPoint target {
        get { return _target; }
        set {
            _target = value;
            if (OnNewTarget != null) {
                OnNewTarget(value);
            }
        }
    }
    public TargetPoint _target;
    public CharacterInput playerController;
    public CharacterState defaultState;
    public CharacterState attackState;
    public CharacterState currentState;


    [HideInInspector]
    public float delta;
    [Header("Time")]
    public float timeScale = 1;

    public delegate void OnTargetFunction(TargetPoint target);
    public event OnTargetFunction OnNewTarget;

    public Ability currentSpell;

    // Use this for initialization
    void Awake () {
        //controller = transform.GetComponent<CharacterController>();
        stats = transform.GetComponent<CharacterStats>();
        combat = transform.GetComponent<CombatController>();
        movement = transform.GetComponent<CharacterMovement>();
        caster2 = transform.GetComponent<AbilityCaster>();
        anim = transform.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody>();
        agent = transform.GetComponent<NavMeshAgent>();
        //comboUser = transform.GetComponent<ComboUser>();
        targetter = transform.GetComponent<PlayerTargetter>();
        combos = transform.GetComponent<SimpleCombo>();

        turnDirection = transform.forward;

        currentState = defaultState;
	}
	
	// Update is called once per frame
	void Update () {
        SetAnimationMovement();
        SetDelta();
        SetTrueSpeed();

        direction = transform.forward;

        if (anim.GetBool("canMove"))
        {
            currentState = defaultState;
        }
        else {
            currentState = attackState;
        }
	}


    /// <summary>
    /// Sets the delta based on timeScale
    /// </summary>
    private void SetDelta() {
        delta = Time.deltaTime * timeScale;
    }

    private void SetAnimationMovement() {
        if (anim != null)
        {
            anim.SetFloat("Horizontal", horizontal);
            anim.SetFloat("Vertical", vertical);
        }
    }


    public void SetTimeScale(float newTimescale) {
        timeScale = newTimescale;
        anim.speed = timeScale;
    }

    public void SetTrueSpeed() {
        trueSpeed = agent.velocity.magnitude;
        try
        {
            anim.SetFloat("Speed", trueSpeed);
        }
        catch (System.Exception e) { }
    }

    public void LockMovement() {
        movementLocked = true;
    }

    public void UnlockMovement() {
        movementLocked = false;
    }


    /// <summary>
    /// Moves the character in the direction relative to the camera
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    public void Move(Vector3 direction, float speed = 5) {
        Vector3 trueDirection = movement.DirectionFromCamera(direction);
        movement.Move(trueDirection, speed);
    }

    /// <summary>
    /// Rotates the character in the direction relative to the character
    /// </summary>
    /// <param name="direction"></param>
    public void Rotate(Vector3 direction) {
        Vector3 trueDirection = movement.DirectionFromCamera(direction);
        movement.SmoothRotate(trueDirection, .7f);
    }


    /// <summary>
    /// Has the player take their turn
    /// </summary>
    public void TakeTurn() {
        if (currentState != null)
        {
            currentState.Execute(this);
        }
    }

}
