using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour {

    public Transform target;
    public Targetable targetHealth;

    private Animator anim;

    public float despawnTime = 30f;
    private float despawnTimer = 0f;

    public float respawnTime = 100f;
    private float respawnTimer = 0f;

    private bool dead = false;

    public Vector3 spawnPoint;

    private NavMeshAgent agent;

    public float maxDistanceFromTarget = 1f;


    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        spawnPoint = transform.position;

        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }

        if (target != null) {
            SetDestination();
            DrawRayToTarget();
            LookAtTarget();
        }
    }


    public void FindTarget() {
        
        Targetable[] targets = GameObject.FindObjectsOfType<Targetable>();

        foreach (Targetable t in targets) {
            if (t.relation != Relation.Enemy) {
                SetTarget(t);
                break;
            }
        }
    }

    public void SetDestination() {
        Vector3 distance = transform.position - target.position;

        agent.SetDestination(target.position);
        agent.stoppingDistance = maxDistanceFromTarget;
    }


    public void LookAtTarget() {
        transform.LookAt(target.position);

        transform.Rotate(new Vector3(-transform.rotation.eulerAngles.x, 0, -transform.rotation.eulerAngles.z));
    }


    public void DrawRayToTarget() {
        Debug.DrawRay(transform.position, agent.destination, Color.blue);
    }


    public void SetTarget(Targetable targetHealth) {
        target = targetHealth.transform;
        this.targetHealth = targetHealth;
    }


    public void Die() {
        agent.destination = transform.position;

        dead = true;

        if (anim != null) {
            //play death animation
        }
    }


    public void Despawn() {
        if (despawnTimer > despawnTime) {
            //coroutine: make object fade out
        }
    }

    public void Respawn() {
        if (respawnTimer > respawnTime) {
            //coroutine: make object fade in
        }
    }
}
