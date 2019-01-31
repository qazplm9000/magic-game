using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AbilitySystem;
using InputSystem;
using CombatSystem;
using StatSystem;
using CharacterStateSystem;
using MovementSystem;
using TargettingSystem;


public class CharacterManager : MonoBehaviour {


    [HideInInspector]
    public float vertical;
    [HideInInspector]
    public float horizontal;

    public Camera mainCamera;


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
    public Targetter targetter;
    public CharacterMovement movement;
    public CharacterRotation rotation;
    public CharacterStats stats;
    public AbilityCaster caster2;
    public SimpleCombo combos;


    public Ability currentAbility;
    public Ability bufferedAbility;
    public float castPrevious = 0;
    public float castCurrent = 0;

    public CharacterVariables vars = new CharacterVariables();

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
    

    public CharacterManager target {
        get { return _target; }
        set {
            _target = value;
            if (OnNewTarget != null) {
                OnNewTarget(value);
            }
        }
    }
    public CharacterManager _target;
    public CharacterInput playerController;
    public CharacterState defaultState;
    public CharacterState attackState;
    public CharacterState currentState;


    [HideInInspector]
    public float delta;
    [Header("Time")]
    public float timeScale = 1;

    public delegate void OnTargetFunction(CharacterManager target);
    public event OnTargetFunction OnNewTarget;


    // Use this for initialization
    void Awake () {
        //controller = transform.GetComponent<CharacterController>();
        stats = transform.GetComponent<CharacterStats>();
        combat = transform.GetComponent<CombatController>();
        caster2 = transform.GetComponent<AbilityCaster>();
        anim = transform.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody>();
        agent = transform.GetComponent<NavMeshAgent>();
        //comboUser = transform.GetComponent<ComboUser>();
        //targetter = transform.GetComponent<PlayerTargetter>();
        combos = transform.GetComponent<SimpleCombo>();

        turnDirection = transform.forward;

        currentState = defaultState;

        //prevents the navmesh agent from auto-turning
        agent.updateRotation = false;

        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        SetAnimationMovement();
        SetDelta();
        SetTrueSpeed();

        direction = transform.forward;

        /*if (anim.GetBool("canMove"))
        {
            currentState = defaultState;
        }
        else {
            currentState = attackState;
        }

        if (currentSpell != null) {
            bool result = currentSpell.Execute(this);
            if (!result) {
                currentSpell = null;
            }
        }*/

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
    public void Move(Vector3 direction) {
        movement.Move(this, direction);
    }

    /// <summary>
    /// Rotates the character in the direction relative to the character
    /// </summary>
    /// <param name="direction"></param>
    public void Rotate(Vector3 direction) {
        Vector3 trueDirection = DirectionFromCamera(direction);
        rotation.Rotate(this, trueDirection);
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



    /// <summary>
    /// Casts the ability if not currently casting
    /// Returns false if already casting
    /// </summary>
    /// <param name="ability"></param>
    /// <returns></returns>
    public bool CastAbility(Ability ability, bool bufferable = true)
    {
        bool result = false;

        if (currentAbility == null)
        {
            currentAbility = ability;
        }
        else {
            if (bufferable) {
                bufferedAbility = ability;
            }
        }

        return result;
    }

    /// <summary>
    /// Runs the current ability to completion and replaces with the buffered ability when done
    /// Returns false when done running
    /// </summary>
    /// <returns></returns>
    public bool RunAbility() {
        bool result = false;

        if (currentAbility != null)
        {
            result = currentAbility.Execute(this);
        }
        else if(bufferedAbility != null){
            currentAbility = bufferedAbility;
            bufferedAbility = null;
            result = true;
        }

        return result;
    }





    /// <summary>
    /// Adjusts the direction in relation to camera
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public Vector3 DirectionFromCamera(Vector3 direction)
    {
        Vector3 camForward = mainCamera.transform.forward;
        camForward = new Vector3(camForward.x, 0, camForward.z);
        camForward /= camForward.magnitude;
        Vector3 camRight = mainCamera.transform.right;

        Vector3 result = camForward * direction.z;
        result += camRight * direction.x;

        if (result.magnitude > 1)
        {
            result /= result.magnitude;
        }

        return result;
    }


}
