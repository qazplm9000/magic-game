using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SkillSystem;

public class CharacterManager : MonoBehaviour {

    public CharacterController controller;
    public CombatController combat;
    public CharacterMovement movement;
    public CharacterStats stats;
    public SkillCaster caster;

    public Animator anim;
    public Rigidbody rb;
    public NavMeshAgent agent;
    public Vector3 direction;

    public bool movementLocked = false;
    public float delta;
    public float timeScale = 1;


	// Use this for initialization
	void Start () {
        controller = transform.GetComponentInChildren<CharacterController>();
        combat = transform.GetComponentInChildren<CombatController>();
        movement = transform.GetComponentInChildren<CharacterMovement>();
        stats = transform.GetComponentInChildren<CharacterStats>();
        caster = transform.GetComponentInChildren<SkillCaster>();
        anim = transform.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody>();
        agent = transform.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        
        SetDelta();
	}


    /// <summary>
    /// Sets the delta based on timeScale
    /// </summary>
    private void SetDelta() {
        delta = Time.deltaTime * timeScale;
    }
}
