using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CombatSystem;
using InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class CombatController : MonoBehaviour {

    CharacterManager characterManager;
    public Action currentState = Action.None;
    /*
    public TargetPoint target;
    public List<TargetPoint> allFriendlyTargets = new List<TargetPoint>();
    public List<TargetPoint> allEnemyTargets = new List<TargetPoint>();
    */
    public delegate void OnAction();
    public OnAction OnGuard;
    public OnAction OnGuardEnd;
    public OnAction OnDodge;
    public OnAction OnDodgeEnd;
    public OnAction OnKnockback;
    public OnAction OnKnockbackEnd;


    public bool interrupted = false;
    public bool invincible = false;
    public bool dodging = false;
    public bool lockedMovement = false;

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
    public float knockbackMinTime = 1f;
    private bool knockbackCancel = false;

    public Action bufferedAction = Action.None;
    public bool bufferOpen = false;


    // Use this for initialization
    void Start () {
        characterManager = GetComponent<CharacterManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
        /*
        if (target != null) {
            Debug.DrawLine(transform.position, target.transform.position);
        }
        */
	}


    /*
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter trigger");
        TargetPoint colliderTarget = other.transform.GetComponent<TargetPoint>();

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
    */
    /*
    private void OnTriggerExit(Collider other)
    {
        TargetPoint colliderTarget = other.transform.GetComponent<TargetPoint>();

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

    */
    /*
    public void GetNearestEnemy() {
        TargetPoint nearestTarget = null;
        float targetProximity = 0;

        foreach (TargetPoint tp in allEnemyTargets) {
            if (nearestTarget == null)
            {
                nearestTarget = tp;
                targetProximity = TargetProximity(tp);
            }
            else {
                if (TargetProximity(tp) < targetProximity)
                {
                    nearestTarget = tp;
                    targetProximity = TargetProximity(tp);
                }
            }
        }

        target = nearestTarget;
        characterManager.target = nearestTarget;
    }
    

    private float TargetProximity(TargetPoint controller) {
        float result = 0;

        result = (controller.transform.position - transform.position).magnitude;

        return result;
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
        else{
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
    */

    public IEnumerator Dodge(Vector3 direction) {

        Vector3 dodgeDirection = direction;

        float dodgeSpeed = dodgeInitialSpeed;
        float dodgeTimer = 0f;
        StartCoroutine(OpenBuffer(0.5f));

        if (dodgeDirection.magnitude == 0)
        {
            dodgeDirection = transform.forward;
        }
        else if (dodgeDirection.magnitude > 1) {
            dodgeDirection /= dodgeDirection.magnitude;
        }

        EnableIFrame();
        currentState = Action.Dodge;

        //animation
        dodging = true;
        //animator.SetBool("Dodge", dodging);
        characterManager.anim.CrossFade("Dodge", 0.2f);

        //activates event OnDodge
        if (OnDodge != null)
        {
            OnDodge();
        }

        //LockMovement();

        while (dodgeSpeed < dodgeTargetSpeed) {
            characterManager.agent.velocity = dodgeDirection * dodgeSpeed;
            dodgeSpeed += dodgeAcceleration * Time.deltaTime;
            dodgeTimer += Time.deltaTime;
            yield return null;
        }

        while (dodgeSpeed > 0) {
            characterManager.agent.velocity = dodgeDirection * dodgeSpeed;
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
                //animator.SetBool("Dodge", dodging);

                if (OnDodgeEnd != null)
                {
                    OnDodgeEnd();
                }

                yield break;
            }

            yield return null;
        }

        dodging = false;
        //animator.SetBool("Dodge", dodging);
        DisableIFrame();
        currentState = Action.None;

        if (OnDodgeEnd != null)
        {
            OnDodgeEnd();
        }

        //UnlockMovement();
    }

    public void Guard() {
        if (OnGuard != null)
        {
            OnGuard();
        }
        currentState = Action.Guard;
        characterManager.isGuarding = true;
        characterManager.anim.SetBool("isGuarding", true);
    }

    public void Unguard() {
        if (OnGuardEnd != null)
        {
            OnGuardEnd();
        }
        characterManager.isGuarding = false;
        currentState = Action.None;
        characterManager.anim.SetBool("isGuarding", false);
    }

    public void EnableIFrame() {
        characterManager.isInvincible = true;
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
            currentState = Action.Knockback;
            ResetKnockback();

            //play knockback animation
            //animator.Play("Knocked Back");

            EnableIFrame();

            while (knockbackTimer < knockbackMaxTime) {
                //push back the character based on amount of knockback
                //if you press a certain key after landing, you break out

                if (knockbackTimer > knockbackMinTime && knockbackCancel) {
                    break;
                }

                knockbackCancel = false;

                yield return null;
            }

            //play standing up animation
            //animator.Play("Stand Up");
            DisableIFrame();
            currentState = Action.None;

            //lock movement while character stands up

        }

        
    }


    public void LockMovement() {
        //lockedMovement = true;
    }

    public void UnlockMovement() {
        //lockedMovement = false;
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


    /// <summary>
    /// Call to cancel knockback animation
    /// Will only work if the minimum time has passed
    /// </summary>
    public void CancelKnockback() {

    }

    /// <summary>
    /// Opens action queue immediately
    /// </summary>
    public void OpenBuffer() {
        bufferOpen = true;
    }

    /// <summary>
    /// Opens action queue after time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator OpenBuffer(float time) {
        float timer = 0f;

        while (timer < time) {
            timer += Time.deltaTime;
            yield return null;
        }

        bufferOpen = true;
    }


    public void TakeDamage(int damage) {
        //decrease HP by damage
        characterManager.stats.TakeDamage(damage);
    }


    public void PlayAnimation(string animationName, int layer = 0) {
        characterManager.anim.CrossFade(animationName, 0.2f);
    }
}
