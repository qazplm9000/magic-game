using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CombatController : MonoBehaviour {

    private CharacterStats userStats;
    private NavMeshAgent characterAgent;
    private SphereCollider targettingCollider;

    public CombatController target;
    public List<CombatController> allFriendlyTargets = new List<CombatController>();
    public List<CombatController> allEnemyTargets = new List<CombatController>();

    public delegate void OnAction();
    public OnAction OnGuard;
    public OnAction OnGuardEnd;
    public OnAction OnDodge;
    public OnAction OnDodgeEnd;
    public OnAction OnKnockback;
    public OnAction OnKnockbackEnd;

    public ComboSystem.ComboHit combo;

    Animator animator;


    public bool interrupted = false;
    public bool invincible = false;
    public bool dodging = false;

    //dodge variables
    public float dodgeInitialSpeed = 5f;
    public float dodgeTargetSpeed = 10f;
    public float dodgeDeacceleration = 15f;
    public float dodgeAcceleration = 25f;
    public float dodgeInvincibleTime = 0.5f;

    //guard variables
    public bool guarding = false;
    public float guardMoveSpeed = 1f;

    //knockback variables
    private float knockback = 0f;
    public float knockbackThreshold = 10f;
    public float knockbackForceMultiplier = 1f;
    public float knockbackMaxTime = 3f; //max time you can stay down before you automatically stand up


    // Use this for initialization
    void Start () {
        characterAgent = transform.GetComponent<NavMeshAgent>();
        userStats = transform.GetComponent<CharacterStats>();
        animator = transform.GetComponentInChildren<Animator>();
        InitTargettingCollider();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Target")) {
            Debug.Log("Targetting nearest enemy");
            GetNearestEnemy();
        }

        if (target != null) {
            Debug.DrawLine(transform.position, target.transform.position);
        }

        
	}



    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter trigger");
        CombatController colliderTarget = other.transform.GetComponent<CombatController>();

        if (colliderTarget != null) {
            if (IsEnemy(other.transform))
            {
                if (!allEnemyTargets.Contains(colliderTarget))
                {
                    allEnemyTargets.Add(colliderTarget);
                }
            }
            else {
                if (!allFriendlyTargets.Contains(colliderTarget))
                {
                    allFriendlyTargets.Add(colliderTarget);
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit trigger");
        CombatController colliderTarget = other.transform.GetComponent<CombatController>();

        if (colliderTarget != null)
        {
            if (IsEnemy(other.transform))
            {
                allEnemyTargets.Remove(colliderTarget);
            }
            else
            {
                allFriendlyTargets.Remove(colliderTarget);
            }
        }
    }



    public void GetNearestEnemy() {
        CombatController nearestTarget = null;
        float targetProximity = 0;

        foreach (CombatController controller in allEnemyTargets) {
            if (nearestTarget == null)
            {
                nearestTarget = controller;
                targetProximity = TargetProximity(controller);
            }
            else {
                if (TargetProximity(controller) < targetProximity)
                {
                    nearestTarget = controller;
                    targetProximity = TargetProximity(controller);
                }
            }
        }

        target = nearestTarget;
    }


    private float TargetProximity(CombatController controller) {
        float result = 0;

        result = (controller.transform.position - transform.position).magnitude;

        return result;
    }


    private void InitTargettingCollider() {
        targettingCollider = transform.gameObject.AddComponent<SphereCollider>();
        targettingCollider.radius = 10f;
        targettingCollider.isTrigger = true;
    }


    private bool IsEnemy(Transform target) {
        bool result = false;

        if (transform.tag == "enemy")
        {
            if (target.tag == "enemy")
            {
                result = false;
            }
            else
            {
                result = true;
            }
        }
        else if (transform.tag == "ally") {
            if (target.tag == "ally") {
                result = false;
            }
            else
            {
                result = true;
            }
        }

        return result;
    }


    public IEnumerator Dodge(Vector3 direction) {

        Vector3 dodgeDirection = direction;

        float dodgeSpeed = dodgeInitialSpeed;
        float dodgeTimer = 0f;

        if (dodgeDirection.magnitude == 0) {
            dodgeDirection = transform.forward;
        }

        EnableIFrame();

        //animation
        dodging = true;
        animator.SetBool("Dodge", dodging);
        animator.Play("Dodge");

        //activates event OnDodge
        OnDodge();

        while (dodgeSpeed < dodgeTargetSpeed) {
            characterAgent.velocity = dodgeDirection * dodgeSpeed;
            dodgeSpeed += dodgeAcceleration * Time.deltaTime;
            dodgeTimer += Time.deltaTime;
            yield return null;
        }

        while (dodgeSpeed > 0) {
            characterAgent.velocity = dodgeDirection * dodgeSpeed;
            dodgeSpeed -= dodgeDeacceleration * Time.deltaTime;

            //checks if invincibility frames need to be disabled
            dodgeTimer += Time.deltaTime;
            if (dodgeTimer > dodgeInvincibleTime)
            {
                DisableIFrame();
            }

            //breaks out if interrupted
            if (interrupted)
            {
                dodging = false;
                animator.SetBool("Dodge", dodging);
                OnDodgeEnd();
                yield break;
            }

            yield return null;
        }

        dodging = false;
        animator.SetBool("Dodge", dodging);
        DisableIFrame();
        OnDodgeEnd();
    }

    public void Guard() {
        OnGuard();
        guarding = true;
        animator.SetBool("Block", guarding);
    }

    public void Unguard() {
        OnGuardEnd();
        guarding = false;
        animator.SetBool("Block", guarding);
    }

    public void EnableIFrame() {
        invincible = true;
    }

    public void DisableIFrame() {
        invincible = false;
    }

    public void Interrupt() {
        interrupted = true;
        if (guarding)
        {
            Unguard();
        }
    }

    /// <summary>
    /// Will knock back the target if amount of knockback received exceeds a certain threshold
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="amount"></param>
    public IEnumerator Knockback(Vector3 direction, float amount) {
        knockback += amount;
        float knockbackTimer = 0f;

        //only knocks back if the total knockback is beyond the threshold
        if (knockback >= knockbackThreshold) {

            ResetKnockback();

            //play knockback animation
            //animator.Play("Knocked Back");

            EnableIFrame();

            while (knockbackTimer < knockbackMaxTime) {
                //push back the character based on amount of knockback
                //if you press a certain key after landing, you break out
                yield return null;
            }

            //play standing up animation
            //animator.Play("Stand Up");
            DisableIFrame();

            //lock movement while character stands up

        }

        
    }

    /// <summary>
    /// Adds knockback to the threshold without triggering a knockback
    /// </summary>
    /// <param name="amount"></param>
    public void AddKnockback(float amount) {
        knockback += amount;
    }

    /// <summary>
    /// Resets the knockback
    /// </summary>
    public void ResetKnockback() {
        knockback = 0;
    }



    public void TakeDamage(int damage) {
        //decrease HP by damage
    }
}
