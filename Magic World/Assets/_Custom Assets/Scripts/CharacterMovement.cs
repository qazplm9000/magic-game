﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using InputSystem;

//Script used for containing all movement functions
public class CharacterMovement : MonoBehaviour {
    
    CharacterManager characterManager;
    [Range(0,1)]
    public float smoothing = 0.7f;

    // Use this for initialization
    void Start () {
        characterManager = transform.GetComponent<CharacterManager>();
        characterManager.agent.updateRotation = false;
	}

    private void Update()
    {
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

    private void _MoveFunction(Vector3 direction, float movementSpeed) {
        //do nothing if direction vector is 0
        if (direction.magnitude == 0)
        {
            characterManager.agent.velocity = Vector3.zero;
            return;
        }

        //rotate character towards direction and change speed
        characterManager.agent.velocity = direction * movementSpeed;
    }

    //take a direction and move towards it
    //movementPercent depends on how far joystick is held
    public void Move(Vector3 direction, float movementSpeed) {
        //direction with Y movement removed
        Vector3 trueDirection = new Vector3(direction.x, 0, direction.z);

        if (trueDirection.magnitude == 0)
        {
            return;
        }

        _MoveFunction(trueDirection, movementSpeed);
        
        SmoothRotate(trueDirection);

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

        _MoveFunction(trueDirection, movementSpeed);

        Vector3 turnDirection = characterManager.target.transform.position - transform.position;
        turnDirection = new Vector3(turnDirection.x, 0, turnDirection.z);

        SmoothRotate(turnDirection);
    }


    /// <summary>
    /// Halt all movement
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="movementSpeed"></param>
    public void HaltMovement() {
        characterManager.agent.velocity = Vector3.zero;
    }


    public void MoveWithoutNavMesh(Vector3 direction, float movementSpeed) {
        //moves the character in the direction
        //transform.position += direction * movementPercent * movementSpeed * Time.deltaTime;
        Vector3 trueDirection = new Vector3(direction.x, 0, direction.z);
        characterManager.rb.velocity = trueDirection * movementSpeed + 
                                            (characterManager.rb.velocity.y * Vector3.down + 
                                            Physics.gravity * Time.deltaTime);
    }
    
    

}
