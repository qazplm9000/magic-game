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
        Debug.Log("Enter trigger");
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
        dodging = true;
        animator.SetBool("Dodge", dodging);
        animator.Play("Dodge");

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
                yield break;
            }

            yield return null;
        }

        dodging = false;
        animator.SetBool("Dodge", dodging);
        DisableIFrame();
    }

    public void Guard() {
        guarding = true;
        animator.SetBool("Block", guarding);
    }

    public void Unguard() {
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
        Unguard();
    }
}
