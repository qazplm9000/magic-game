using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Script used for containing all movement functions
public class CharacterMovement : MonoBehaviour {

    float movementSpeed = 5f;
    bool movementLocked = false;
    new Camera camera;
    NavMeshAgent agent;
    public float lockTimer = 0f;
    Animator animator;

    // Use this for initialization
    void Start () {
        camera = Camera.main;
        agent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponentInChildren<Animator>();
        agent.updateRotation = false;
	}

    

    public void Rotate(Vector3 direction) {
        //get direction based on direction and camera direction
        //Vector3 cameraDirection = mainCamera.transform.forward;
        //cameraDirection.y = 0;

        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        angle = Mathf.LerpAngle(0, angle, 0.5f);
        transform.Rotate(transform.up, angle);
    }

    //take a direction and move towards it
    //movementPercent depends on how far joystick is held
    public void Move(Vector3 direction, float movementSpeed) {

        //don't do anything if movement is locked
        if (movementLocked) {
            return;
        }
        
        //direction with Y movement removed
        Vector3 trueDirection = new Vector3(direction.x, 0, direction.z);

        
        //do nothing if direction vector is 0
        if (trueDirection.magnitude == 0) {
            agent.velocity = Vector3.zero;
            animator.SetFloat("Speed", 0);
            return;
        }

        animator.SetFloat("Speed", movementSpeed);

        //rotate character towards direction and change speed
        Rotate(trueDirection);
        agent.velocity = trueDirection * movementSpeed;

    }

    public void MoveWithoutNavMesh(Vector3 direction, float movementPercent = 1.0f) {

        if (!movementLocked && direction.magnitude != 0)
        {
            //moves the character in the direction
            transform.position += direction * movementPercent * movementSpeed * Time.deltaTime;
            Rotate(direction);
        }
    }

    /// <summary>
    /// Gets a direction from the input
    /// </summary>
    /// <returns></returns>
    public Vector3 DirectionFromInput()
    {

        Vector3 direction = camera.transform.forward * Input.GetAxis("Vertical");
        direction += camera.transform.right * Input.GetAxis("Horizontal");

        if (direction.magnitude != 0)
        {
            direction /= direction.magnitude;
        }

        return direction;

    }
    

}
