using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using InputSystem;

//Script used for containing all movement functions
public class CharacterMovement : MonoBehaviour {

    float movementSpeed = 5f;
    bool movementLocked = false;
    new Camera camera;
    NavMeshAgent agent;
    public float lockTimer = 0f;
    Animator animator;
    private Rigidbody rb;

    public float smoothing = 0.7f;

    // Use this for initialization
    void Start () {
        camera = Camera.main;
        agent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody>();
        agent.updateRotation = false;
	}

    
    /// <summary>
    /// Smoothly rotate character
    /// </summary>
    /// <param name="direction"></param>
    public void SmoothRotate(Vector3 direction) {
        //get direction based on direction and camera direction
        //Vector3 cameraDirection = mainCamera.transform.forward;
        //cameraDirection.y = 0;

        if (direction.magnitude != 0)
        {
            float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            angle = Mathf.LerpAngle(0, angle, 0.5f);
            transform.Rotate(transform.up, angle);
        }
    }

    /// <summary>
    /// Rotate without smoothing
    /// </summary>
    /// <param name="direction"></param>
    public void Rotate(Vector3 direction) {
        if (direction.magnitude != 0)
        {
            float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            transform.Rotate(transform.up, angle);
        }
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
            return;
        }

        //rotate character towards direction and change speed
        SmoothRotate(trueDirection);
        agent.velocity = trueDirection * movementSpeed;

    }

    public void MoveWithoutNavMesh(Vector3 direction, float movementSpeed) {
        //moves the character in the direction
        //transform.position += direction * movementPercent * movementSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, transform.position + direction * movementSpeed * Time.deltaTime, smoothing);
        SmoothRotate(direction);
    }


    public void OnCollisionStay(Collision collision)
    {
        Vector3 normal = Vector3.zero;
        foreach (ContactPoint contact in collision.contacts) {
            normal = contact.normal;
        }

        float angleBetween = 0;

        if (normal.magnitude != 0) {
            angleBetween = Vector3.Angle(normal, Vector3.down);
        }

        rb.AddForce(-normal * Mathf.Sin(angleBetween));
    }

    /// <summary>
    /// Gets a direction from the input
    /// </summary>
    /// <returns></returns>
    public Vector3 DirectionFromInput()
    {
        Vector3 cameraForward = camera.transform.forward;
        cameraForward /= cameraForward.magnitude;
        Vector3 cameraRight = camera.transform.right;
        cameraRight /= cameraRight.magnitude;
        Vector3 direction = cameraForward * InputManager.manager.GetAxis("Vertical Left");
        direction += cameraRight * InputManager.manager.GetAxis("Horizontal Left");

        if (direction.magnitude > 1)
        {
            direction /= direction.magnitude;
        }

        return direction;

    }
    

}
