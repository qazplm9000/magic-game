using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AbilitySystem;
using InputSystem;
//using CombatSystem;
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
using HitboxSystem;

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
    private HitboxManager hitbox;

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


    public CharacterManager target;

    

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
        hitbox = transform.GetComponent<HitboxManager>();

        //prevents the navmesh agent from auto-turning
        //agent.updateRotation = false;

        mainCamera = Camera.main;
    }

    public void Start()
    {
        //World.eventManager.SubscribeEvent("OnSwitchNextButton", SwitchNextCombo);
        //World.eventManager.SubscribeEvent("OnSwitchPreviousButton", SwitchPreviousCombo);
    }
    
	
	// Update is called once per frame
	void Update () {
        SetAnimationMovement();
        SetTrueSpeed();
        //direction = transform.forward;
        

	}




    public void OnTurnStart(BattleContext battle)
    {
        //RaiseEvent("OnTurnStart");

        //statusManager.Tick();
    }

    /// <summary>
    /// Called every frame during combat
    /// </summary>
    public void TakeTurn(BattleContext battle)
    {
        /*if (!characterController.isPlayer)
        {
            characterController.CallInput();
        }

        stateManager.UpdateState();*/

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position +=   vertical   * movementSpeed * Time.deltaTime * transform.forward +
                                horizontal * movementSpeed * Time.deltaTime * transform.right;


        if (Input.GetKeyDown(KeyCode.K)) {
            Debug.Log("Attacked target");
            //starts casting skill
            //when dodge time starts, warn 
        }
    }

    public void OnTurnEnd(BattleContext battle) {

    }


    //Defend functions

    /// <summary>
    /// Called as soon as the character is targetted by a skill
    /// </summary>
    /// <param name="battle"></param>
    public void OnDefend(BattleContext battle) {
        //for player, throw a prompt to defend
        //for enemy, maybe get a random float for when to defend
            //Time should depend on chance of defending and how long before attack lands
    }

    /// <summary>
    /// Called every frame when a character is being targetted by a skill
    /// </summary>
    /// <param name="battle"></param>
    public void Defend(BattleContext battle, float remainingTime)
    {
        //player:
        //press button to defend, dodge, or counter
        //maybe defend can be held down and blocks partial damage, but can't block attacks from behind
            //should have some way to modify this easily
        //dodge avoids all damage and can face any direction
        //counter does damage, maybe has to be after a dodge
    }

    public void OnDefendEnd(BattleContext battle) {
        //go back to idle animation
    }


    //Idle functions

    public void OnIdle(BattleContext battle) {

    }

    public void Idle(BattleContext battle)
    {

    }

    public void OnIdleEnd(BattleContext battle) {

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
    public void MoveInDirection(Vector3 direction, float distance = 9999)
    {
        movement.MoveInDirection(direction, distance);
    }

    public void MoveForward() {
        movement.MoveForward();
    }


    public void FaceDirection(Vector3 direction) {
        movement.FaceDirection(direction);
    }

    /// <summary>
    /// Rotates the character in the direction relative to the character
    /// </summary>
    /// <param name="direction"></param>
    public void Rotate(float angle)
    {
        movement.Rotate(angle);
    }


    public bool SetDestination(Vector3 destination) {
        return characterController.SetDestination(destination);
    }

    public bool SetDestinationToNearestPoint() {
        return characterController.SetDestinationToNearestPoint(GetTarget());
    }

    public bool MoveTowardsDestination() {
        return characterController.MoveTowardsDestination();
    }

    public bool HasPath() {
        return characterController.HasPath();
    }


    public Vector3 GetVelocity() {
        return movement.GetHorizontalVelocity();
    }

    public void SetVelocity(Vector3 newVelocity) {
        movement.SetHorizontalVelocity(newVelocity);
    }


    public float GetCurrentSpeed() {
        return GetVelocity().magnitude;
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
    /// Sets the character's next action to attacking
    /// </summary>
    /// <returns></returns>
    public void Attack()
    {
        abilityCaster.Attack();
    }

    public void Cast(int index) {
        abilityCaster.SelectSkill(index);
    }

    public void SwitchNextCombo() {
        abilityCaster.IncrementPreset(1);
        Debug.Log("Switched next combo");
    }

    public void SwitchPreviousCombo() {
        abilityCaster.IncrementPreset(-1);
        Debug.Log("Switched previous");
    }

    public void ResetCombo() {
        abilityCaster.ResetCurrentCombo();
    }


    /// <summary>
    /// Runs the current ability to completion
    /// Returns false when done running
    /// Raises OnFinishCast event
    /// </summary>
    /// <returns></returns>
    public void RunAbility()
    {
        abilityCaster.PlayCurrentAbility();    
    }

    /// <summary>
    /// Returns true if character is not casting
    /// </summary>
    /// <returns></returns>
    public bool IsCasting() {
        return abilityCaster.IsUsingAbility();
    }

    /// <summary>
    /// Returns true if ability is a combo
    /// </summary>
    /// <returns></returns>
    public bool AbilityIsCombo() {
        return abilityCaster.IsCombo();
    }


    /// <summary>
    /// Returns true if character can move to Idle state
    /// </summary>
    /// <param name="ability"></param>
    /// <returns></returns>
    public bool NotTakingAction()
    {
        return stateManager.currentState.StateName == "Active";
    }

    /// <summary>
    /// Returns true if character is currently taking their turn and turn timer > 0
    /// </summary>
    /// <returns></returns>
    public bool IsTakingTurn() {
        return true;//World.battle.currentTurn == this && World.battle.turnTime > 0;
    }

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }

    public void StaggerDamage(int damage) {
        stats.StaggerDamage(damage);
    }

    

    

    public bool IsEnemy(CharacterManager character)
    {
        return false;
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

    public int CalculateFirstTurn() {
        return stats.CalculateFirstTurn();
    }

    public int CalculateNextTurn(int previousTurn) {
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


    public AllowedActions GetAllowedActions() {
        return stateManager.currentState.allowedActions;
    }


    public bool ActionAllowed(CharacterAction action) {
        AllowedActions allowedActions = GetAllowedActions();
        return allowedActions.ActionIsAllowed(action);
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


    #region Hitbox

    public Vector3 GetNearestPointFromDirection(Vector3 direction) {
        return hitbox.GetNearestPointFromDirection(direction);
    }

    public Vector3 GetNearestPointFromPosition(Vector3 position) {
        return hitbox.GetNearestPointFromPosition(position);
    }

    public float GetDistanceFromFront() {
        return hitbox.GetDistanceFromFront();
    }

    #endregion



    /// <summary>
    /// Returns time for character's next turn
    /// </summary>
    /// <returns></returns>
    public int NextTurnTime() {
        return 0;
    }

    /// <summary>
    /// Returns time for character's future turn
    /// 1 turn = turn after next
    /// 2 turns = turn after
    /// etc.
    /// </summary>
    /// <param name="turns"></param>
    /// <returns></returns>
    public int FutureTurnTime(int turns = 1) {
        return 1000 * turns;
    }

    /// <summary>
    /// Gets the time for an upcoming turn
    /// turnNumber = 0 means next turn
    /// turnNumber = 1 means 1 turn later
    /// </summary>
    /// <param name="turnsInFuture"></param>
    /// <returns></returns>
    public int GetTurnTime(int turnNumber = 0) {
        //turnTime + period * turnNumber
        //Can consider things like speed buffs etc later on
        return 0;
    }

}
