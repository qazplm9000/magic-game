using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using InputSystem;

//Script used for containing all movement functions
public class CharacterMovement : MonoBehaviour {
    
    private CharacterManager character;
    private Camera mainCamera;

    [Range(0,1)]
    public float smoothing = 0.7f;

    // Use this for initialization
    void Start () {
        character = transform.GetComponent<CharacterManager>();
        mainCamera = Camera.main;
        //prevents the navmesh agent from auto-turning
        character.agent.updateRotation = false;
	}
    

    /// <summary>
    /// Smoothly rotate character
    /// </summary>
    /// <param name="newDirection"></param>
    public void SmoothRotate(Vector3 direction, float turnSpeed) {
        //get direction based on direction and camera direction
        //Vector3 cameraDirection = mainCamera.transform.forward;
        //cameraDirection.y = 0;
        Vector3 newDirection = new Vector3(direction.x, 0, direction.z);

        if (newDirection.magnitude != 0)
        {
            float angle = Vector3.SignedAngle(transform.forward, newDirection, Vector3.up);
            angle = Mathf.LerpAngle(0, angle, turnSpeed);
            transform.Rotate(transform.up, angle);
        }
    }

    //Smoothly rotate character
    public void SmoothRotate(Vector3 direction) {
        SmoothRotate(direction, smoothing);
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
    public void Move(Vector3 direction, float movementSpeed = 5) {
        //direction with Y movement removed
        //Vector3 trueDirection = new Vector3(direction.x, 0, direction.z);

        character.agent.velocity = direction * movementSpeed;
        //SmoothRotate(trueDirection);

    }

    
    public Vector3 DirectionFromCamera(Vector3 direction) {
        Vector3 result = mainCamera.transform.forward * direction.z;
        result += mainCamera.transform.right * direction.x;

        if (result.magnitude > 1) {
            result /= result.magnitude;
        }

        return result;
    }


    /// <summary>
    /// Moves while facing the target
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="movementSpeed"></param>
    public void Strafe(Vector3 direction, float movementSpeed) {
        //direction with Y movement removed
        Vector3 trueDirection = new Vector3(direction.x, 0, direction.z);

        if (trueDirection.magnitude == 0) {
            return;
        }

        //_MoveFunction(trueDirection, movementSpeed);
        SmoothRotate(character.target.transform.position - transform.position);
    }


    /// <summary>
    /// Halt all movement
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="movementSpeed"></param>
    public void HaltMovement() {
        character.agent.velocity = Vector3.zero;
    }


    public void MoveWithoutNavMesh(Vector3 direction, float movementSpeed) {
        //moves the character in the direction
        //transform.position += direction * movementPercent * movementSpeed * Time.deltaTime;
        Vector3 trueDirection = new Vector3(direction.x, 0, direction.z);
        character.rb.velocity = trueDirection * movementSpeed + 
                                            (character.rb.velocity.y * Vector3.down + 
                                            Physics.gravity * Time.deltaTime);
    }
    

}
