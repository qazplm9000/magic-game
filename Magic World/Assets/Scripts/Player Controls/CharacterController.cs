using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour {

    public float speed = 5f;

    public float jumpSpeed = 5f;
    private float jumpVelocity = 0f;
    public float terminalVelocity = 40f;
    private bool grounded = true;

    private Rigidbody rb;

    private Camera camera;

    public GameObject characterModel;

    void Start()
    {
        camera = Camera.main;
        rb = transform.GetComponent<Rigidbody>();
    }


    void Update()
    {
        Turn();
        Move();
    }

    void FixedUpdate()
    {
        Jump();

    }




    private void OnCollisionStay(Collision collision)
    {
        grounded = false;
        foreach (ContactPoint contact in collision.contacts) {
            if (contact.point.y < (transform.position.y + 0.5f)) {
                grounded = true;
                rb.useGravity = true;
                break;
            }
        }
    }


    public void Move()
    {
        transform.position += GetMovementVector() * speed * Time.deltaTime;
    }

    public void Turn() {
        Vector3 movementVector = GetMovementVector();
        transform.LookAt(transform.position + movementVector);
    }


    public void Jump() {
        if (Input.GetButtonDown("Jump") && grounded) {
            jumpVelocity = jumpSpeed;
            grounded = false;
            rb.useGravity = false;
        }

        if (!grounded)
        {
            transform.position += transform.up * jumpVelocity * Time.deltaTime;
            jumpVelocity += Physics.gravity.y * Time.deltaTime;

            if (jumpVelocity < -terminalVelocity) {
                jumpVelocity = -terminalVelocity;
            }
        }
    }

    


    public Vector3 GetMovementVector() {
        Vector3 forwardMovement = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z) * Input.GetAxis("Vertical");
        Vector3 horizontalMovement = camera.transform.right * Input.GetAxis("Horizontal");

        Vector3 movementVector = forwardMovement + horizontalMovement;
        movementVector = movementVector.magnitude == 0 ? new Vector3(0, 0, 0) : movementVector / movementVector.magnitude;

        return movementVector;
    }

    
}
