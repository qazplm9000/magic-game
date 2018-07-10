using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DialogueSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class TownController : MonoBehaviour {

    public float speed = 5.0f;

    private new Camera camera;
    private NavMeshAgent agent;

    private Vector3 facingDirection;
    private Vector3 leftDirection;


    private bool canMove = true;

    private void Start()
    {
        camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        UpdateDirection(transform.forward);

        DialogueHandler.OnDialogueStart += DisableMovement;
        DialogueHandler.OnDialogueEnd += EnableMovement;
    }



    private void Update()
    {

        Debug.DrawRay(transform.position + new Vector3(0, 1.8f, 0), transform.forward * 10);

        if (canMove)
        {
            Move();
            Rotate();
        }

        UpdateDirection(camera.transform.forward);
    }


    public void DisableMovement() {
        canMove = false;
        agent.velocity = 0 * CalculateDirectionVector();
    }

    public void EnableMovement() {
        canMove = true;
    }

    public void Move()
    {
        agent.velocity = CalculateDirectionVector() * speed;
    }

    public void Rotate() {
        Vector3 newDirection = CalculateDirectionVector();
        if(newDirection.magnitude == 0)
        {
            return;
        }

        float angleDelta = Vector3.SignedAngle(transform.forward, newDirection, transform.up);
        transform.Rotate(new Vector3(0, angleDelta, 0));
    }


    public void UpdateDirection(Vector3 forwardVector)
    {
        facingDirection = new Vector3(forwardVector.x, 0, forwardVector.z);
        leftDirection = Vector3.Cross(facingDirection, transform.up);
    }

    public Vector3 CalculateDirectionVector() {
        Vector3 result = new Vector3();

        result += facingDirection * Input.GetAxis("Vertical");
        result -= leftDirection * Input.GetAxis("Horizontal");

        if (result.magnitude != 0) {
            result /= result.magnitude;
        }

        return result;
    }

}
