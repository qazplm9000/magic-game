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
using BuffSystem;
using PartySystem;
using BattleSystem;
using AnimationSystem;
using InventorySystem;

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
    private TargetManager targetter;
    private MovementManager movement;
    private CharacterStats stats;
    private AbilityManager abilityCaster;
    private CharacterStateManager stateManager;
    private CharacterEventManager eventManager;
    private CharacterController characterController;
    private StatusManager statusManager;
    private AnimationManager animationManager;
    private Inventory inventory;

    public Party party;
    public BattleManager battleState;

    public CharacterAction currentAction;
    public CharacterAction bufferedAction;

    /*public Ability currentAbility;
    public Ability bufferedAbility;*/

    public AllowedActions allowedActions;

    //public CharacterVariables vars = new CharacterVariables();

    //used for allowing different character models
    //public GameObject activeModel;

    /*  Eventually used to load in a character
     *  Should include:
     *      Character Model
     *      Stats
     *      AI
     *      
     */
    //public CharacterPreset characterPreset;
    

    //[HideInInspector]
    //public Animator anim;
    //[HideInInspector]
    //public Rigidbody rb;
    //[HideInInspector]
    //public NavMeshAgent agent;
    //[HideInInspector]
    //public Vector3 direction;
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
        //anim = transform.GetComponentInChildren<Animator>();
        //rb = transform.GetComponent<Rigidbody>();
        //agent = transform.GetComponent<NavMeshAgent>();
        abilityCaster = transform.GetComponent<AbilityManager>();
        movement = transform.GetComponent<MovementManager>();
        stateManager = transform.GetComponent<CharacterStateManager>();
        eventManager = transform.GetComponent<CharacterEventManager>();
        characterController = transform.GetComponent<CharacterController>();
        targetter = transform.GetComponent<TargetManager>();
        stats = transform.GetComponent<CharacterStats>();
        statusManager = transform.GetComponent<StatusManager>();
        animationManager = transform.GetComponent<AnimationManager>();

        //prevents the navmesh agent from auto-turning
        //agent.updateRotation = false;

        mainCamera = Camera.main;
    }

    public void Start()
    {
        //World.eventManager.SubscribeEvent("OnSwitchNextButton", SwitchNextCombo);
        //World.eventManager.SubscribeEvent("OnSwitchPreviousButton", SwitchPreviousCombo);
        World.world.input1.OnInput += characterController.ReceiveInput;
        World.world.input2.OnInput += characterController.ReceiveInput;
    }
    
	
	// Update is called once per frame
	void Update () {
        SetAnimationMovement();
        SetTrueSpeed();

        //direction = transform.forward;
        

	}

    

    

    






    









    #region Movement

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
    public void MoveInDirection(Vector3 direction)
    {
        movement.MoveInDirection(World.world.mainCamera, direction);
    }

    /// <summary>
    /// Rotates the character in the direction relative to the character
    /// </summary>
    /// <param name="direction"></param>
    public void Rotate()
    {
        movement.Rotate();
    }


    public Vector3 GetVelocity() {
        return movement.GetHorizontalVelocity();
    }

    public void SetVelocity(Vector3 newVelocity) {
        movement.SetHorizontalVelocity(newVelocity);
    }


    #endregion

    
    #region Combat


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

        return attacking;
    }

    public void Cast(int index) {
        abilityCaster.SelectSkill(index);
    }

    public void SwitchNextCombo() {
        abilityCaster.SwitchNextPreset();
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
    /// Runs the current ability to completion
    /// Returns false when done running
    /// Raises OnFinishCast event
    /// </summary>
    /// <returns></returns>
    public bool RunAbility()
    {
        bool playing = false;

        if (abilityCaster.currentSkill != null)
        {
            playing = abilityCaster.Cast();

            if (!playing)
            {
                eventManager.RaiseEvent("OnFinishCast");
            }
        }
        
        return abilityCaster.Cast();
    }

    /// <summary>
    /// Casts the ability if not currently casting
    /// Returns false if already casting
    /// </summary>
    /// <param name="ability"></param>
    /// <returns></returns>
    public bool NotTakingAction()
    {
        return stateManager.currentState.StateName == "Active";
    }


    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }

    public void StaggerDamage(int damage) {
        stats.StaggerDamage(damage);
    }

    public void StartTurn() {
        RaiseEvent("OnTurnStart");

        statusManager.Tick();
    }




    public void RemoveStatus(StatusEffect status) {
        statusManager.RemoveStatus(status);
    }



    #endregion


    #region Stats

    //***********************************************//
    //***********************************************//
    //*************                ******************//
    //************  Stat Functions  *****************//
    //*************                ******************//
    //***********************************************//
    //***********************************************//


    public void UseMana(int mana) {
        party.UseMana(mana);
    }

    public void RestoreMana(int mana) {
        party.RestoreMana(mana);
    }

    public bool HasEnoughMana(int mana) {
        return party.HasEnoughMana(mana);
    }

    //Likely make get functions inside CharacterStats that calculates the current total

    public int GetMaxHealth() {
        return stats.maxHealth;
    }

    public int GetCurrentHealth() {
        return stats.maxHealth;
    }

    public bool IsDead() {
        return stats.IsDead();
    }

    //Maybe add an enum in args to choose between strength or magic for attack stat (and maybe others ones as well)
    public int GetAttackStat() {
        return stats.strength;
    }

    public int GetAgility() {
        return stats.agility;
    }

    public float GetAttackTime() {
        return stats.attackTime;
    }

    public AbilityElement GetElement() {
        return stats.element;
    }

    #endregion


    #region Inventory

    //*************************************************//
    //*************************************************//
    //***********                     *****************//
    //**********  Inventory Functions  ****************//
    //***********                     *****************//
    //*************************************************//
    //*************************************************//

    public void RemoveItem(Item item, int quantity = 1) {
        inventory.RemoveItem(item, quantity);
    }

    public void AddItem(Item item, int quantity = 1) {
        inventory.AddItem(item, quantity);
    }

    public bool HasEnoughOfItem(Item item, int quantity = 1) {
        return inventory.HasEnoughOfItem(item, quantity);
    }

    #endregion


    #region Targetting

    //*************************************************//
    //*************************************************//
    //***********                     *****************//
    //**********  Targetting Functions  ****************//
    //***********                     *****************//
    //*************************************************//
    //*************************************************//


    public CharacterManager GetTarget() {
        return targetter.GetNextTarget();
    }

    public float CalculateFirstTurn() {
        return stats.CalculateFirstTurn();
    }

    public float CalculateNextTurn(float previousTurn) {
        return stats.CalculateNextTurn(previousTurn);
    }

    #endregion


    #region Animation

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
        animationManager.PlayAnimation(animationName, layer);
    }


    private void SetAnimationMovement()
    {
        /*if (anim != null)
        {
            anim.SetFloat("Horizontal", horizontal);
            anim.SetFloat("Vertical", vertical);
        }*/
    }



    public void SetTrueSpeed()
    {
        /*trueSpeed = rb.velocity.magnitude;
        try
        {
            anim.SetFloat("Speed", trueSpeed);
        }
        catch (System.Exception e) { }*/
    }

    #endregion


    #region States

    //*************************************************//
    //*************************************************//
    //*************                 *******************//
    //************  State Functions  ******************//
    //*************                 *******************//
    //*************************************************//
    //*************************************************//



    public float GetStateTime() {
        return stateManager.stateTime;
    }

    #endregion


    #region Events

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

    #endregion
}
