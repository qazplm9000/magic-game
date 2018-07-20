using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SkillSystem;
using InputSystem;
using CombatSystem;

public class CharacterManager : MonoBehaviour {


    [HideInInspector]
    public float vertical;
    [HideInInspector]
    public float horizontal;

    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public CombatController combat;
    [HideInInspector]
    public CharacterMovement movement;
    [HideInInspector]
    public CharacterStats stats;
    [HideInInspector]
    public SkillCaster caster;

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

    [Header("Movement")]
    public float movementSpeed = 5;
    public bool movementLocked = false;
    public float trueSpeed = 0;
    [Range(0,1)]
    public float guardSpeedMultiplier = 0.2f;
    [Range(0,1)]
    public float strafeSpeedMultiplier = 0.7f;

    [Header("Combat")]
    public bool bufferOpen = true;
    public Action bufferedAction = Action.None;
    public bool isDead = false;
    public bool isInvincible = false;
    public bool isGuarding = false;
    public bool isLockingOnTarget = false;
    public CharacterManager target;

    [HideInInspector]
    public float delta;
    [Header("Time")]
    public float timeScale = 1;


    


    // Use this for initialization
    void Awake () {
        controller = transform.GetComponent<CharacterController>();
        combat = transform.GetComponent<CombatController>();
        movement = transform.GetComponent<CharacterMovement>();
        stats = transform.GetComponent<CharacterStats>();
        caster = transform.GetComponent<SkillCaster>();
        anim = transform.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody>();
        agent = transform.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        SetAnimationMovement();
        SetDelta();
        
	}


    /// <summary>
    /// Sets the delta based on timeScale
    /// </summary>
    private void SetDelta() {
        delta = Time.deltaTime * timeScale;
    }

    private void SetAnimationMovement() {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
    }


    public void SetTimeScale(float newTimescale) {
        timeScale = newTimescale;
        anim.speed = timeScale;
    }

    
}
