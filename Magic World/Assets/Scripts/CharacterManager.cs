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
using EventSystem;


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
    public MovementManager movement;
    public CharacterRotation rotation;
    public CharacterStats stats;
    public ComboManager combos;
    public AbilityManager abilityCaster;
    public CharacterStateManager stateManager;
    public CharacterEventManager eventManager;

    public CharacterAction currentAction;
    public CharacterAction bufferedAction;

    public Ability currentAbility;
    public Ability bufferedAbility;

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

    

    public delegate void CharacterEvent(CharacterManager character);
    public CharacterEvent OnNewTarget;


    // Use this for initialization
    void Awake () {
        //controller = transform.GetComponent<CharacterController>();
        combat = transform.GetComponent<CombatController>();
        anim = transform.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody>();
        agent = transform.GetComponent<NavMeshAgent>();
        //comboUser = transform.GetComponent<ComboUser>();
        //targetter = transform.GetComponent<PlayerTargetter>();
        combos = transform.GetComponent<ComboManager>();
        abilityCaster = transform.GetComponent<AbilityManager>();
        movement = transform.GetComponent<MovementManager>();
        stateManager = transform.GetComponent<CharacterStateManager>();
        eventManager = transform.GetComponent<CharacterEventManager>();

        //prevents the navmesh agent from auto-turning
        agent.updateRotation = false;

        mainCamera = Camera.main;
    }

    public void Start()
    {
        World.eventManager.SubscribeEvent("OnSwitchNextButton", SwitchNextCombo);
        World.eventManager.SubscribeEvent("OnSwitchPreviousButton", SwitchPreviousCombo);
    }
    
	
	// Update is called once per frame
	void Update () {
        SetAnimationMovement();
        SetTrueSpeed();

        direction = transform.forward;
        

	}

    

    private void SetAnimationMovement() {
        if (anim != null)
        {
            anim.SetFloat("Horizontal", horizontal);
            anim.SetFloat("Vertical", vertical);
        }
    }

    

    public void SetTrueSpeed() {
        trueSpeed = rb.velocity.magnitude;
        try
        {
            anim.SetFloat("Speed", trueSpeed);
        }
        catch (System.Exception e) { }
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
    










    //*************************************************//
    //*************************************************//
    //*************                    ****************//
    //************  Movement Functions  ***************//
    //*************                    ****************//
    //*************************************************//
    //*************************************************//



    /// <summary>
    /// Moves the character in the direction relative to the camera
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    public void Move(Vector3 direction)
    {
        movement.Move(direction);
    }

    /// <summary>
    /// Rotates the character in the direction relative to the character
    /// </summary>
    /// <param name="direction"></param>
    public void Rotate()
    {
        movement.Rotate();
    }














    //*************************************************//
    //*************************************************//
    //*************                  ******************//
    //************  Combat Functions  *****************//
    //*************                  ******************//
    //*************************************************//
    //*************************************************//



    /// <summary>
    /// Plays the current combo in the combo manager
    /// </summary>
    /// <returns></returns>
    public bool Attack()
    {
        bool attacking = abilityCaster.Attack();

        if (!attacking) {
            eventManager.RaiseEvent("OnFinishAttack");
        }

        return abilityCaster.Attack();
    }

    public void SwitchNextCombo() {
        abilityCaster.SwitchNextCombo();
        Debug.Log("Switched next combo");
    }

    public void SwitchPreviousCombo() {
        abilityCaster.SwitchPreviousCombo();
        Debug.Log("Switched previous");
    }

    public void ResetCombo() {
        abilityCaster.ResetCombo();
    }


    /// <summary>
    /// Runs the current ability to completion and replaces with the buffered ability when done
    /// Returns false when done running
    /// </summary>
    /// <returns></returns>
    public bool RunAbility()
    {
        bool playing = abilityCaster.Cast();

        if (!playing) {
            eventManager.RaiseEvent("OnFinishCast");
        }

        return abilityCaster.Cast();
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
        else
        {
            if (bufferable)
            {
                bufferedAbility = ability;
            }
        }

        return result;
    }


    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }








    //*************************************************//
    //*************************************************//
    //***********                     *****************//
    //**********  Animation Functions  ****************//
    //***********                     *****************//
    //*************************************************//
    //*************************************************//



    /// <summary>
    /// Plays the animation
    /// </summary>
    /// <param name="animationName"></param>
    public void PlayAnimation(string animationName, int layer = 0)
    {
        //later on maybe extend functionality to a separate animation class
        //  that allows the same animator to be used across characters with separate
        //  but with different animations assigned to different names
        anim.CrossFade(animationName, 0.1f, layer);
    }




    //*************************************************//
    //*************************************************//
    //*************                 *******************//
    //************  Event Functions  ******************//
    //*************                 *******************//
    //*************************************************//
    //*************************************************//

    public void RaiseEvent(string eventName) {
        eventManager.RaiseEvent(eventName);
    }

    public void SubscribeEvent(string eventName, CharacterEventHandler.CharacterEvent method) {
        eventManager.SubscribeEvent(eventName, method);
    }

    public void UnsubscribeEvent(string eventName, CharacterEventHandler.CharacterEvent method) {
        eventManager.UnsubscribeEvent(eventName, method);
    }

}
