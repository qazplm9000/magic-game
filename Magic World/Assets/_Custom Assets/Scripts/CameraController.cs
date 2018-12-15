using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

public class CameraController : MonoBehaviour {

    public TargetPoint player;
    private TargetPoint target;
    private Vector3 direction;

    public float initialDistance = 5;
    [Range(0,60)]
    public float initialAngle = 30;
    public Vector3 offset;
    public float xAngle;
    public float yAngle;

    public float xAngleSpeed = 5;
    public float yAngleSpeed = 5;


    private Vector3 currentVelocity;
    [Range(0,1)]
    public float smoothTime = 0.1f;
    [Range(0,1)]
    public float rotationDamping = 0.1f;



	// Use this for initialization
	void Start () {
        //initialize variables
        xAngle = 0;
        yAngle = initialAngle;
        direction = -player.transform.forward;
        transform.position = transform.position + offset + CalculateVector(player.transform.forward, initialDistance, xAngle, yAngle);

        //put UpdateTarget in event
        player.manager.OnNewTarget += UpdateTarget;
        FaceTarget(player.transform);
	}
	
	// Update is called once per frame
	void Update () {
        //Get new angles from player input
        xAngle += World.inputs.GetAxis("Horizontal Right") * Time.deltaTime * xAngleSpeed;
        yAngle += World.inputs.GetAxis("Vertical Right") * Time.deltaTime * yAngleSpeed;

        if (target == null)
        {
            //Clamp y angle
            yAngle = Mathf.Clamp(yAngle, -60, 60);

            //Calculate the new location of the camera
            Vector3 newOffset = CalculateVector(direction, initialDistance, xAngle, yAngle);
            
            //dampen the movement
            Vector3 newLocation = Vector3.SmoothDamp(transform.position + offset, 
                                                    player.transform.position + offset + newOffset, 
                                                    ref currentVelocity, smoothTime);

            //Set new location
            transform.position = newLocation;

            //Face target
            FaceTarget(player.transform);
        }
        else {
            //update direction
            direction = -player.transform.forward;
            
            //Clamp X and Y angles
            xAngle = Mathf.Clamp(xAngle, -30, 30);
            yAngle = Mathf.Clamp(yAngle, -30, 30);

            //get new offset
            Vector3 newOffset = CalculateVector(direction, initialDistance, xAngle, yAngle);

            //dampen movement
            Vector3 newPosition = Vector3.SmoothDamp(transform.position + offset, 
                                                    player.transform.position + offset + newOffset, 
                                                    ref currentVelocity, smoothTime);

            //Set new location
            transform.position = newPosition;

            //face target
            FaceTarget(target.transform);
        }
        
	}


    private void UpdateTarget(TargetPoint newTarget) {
        target = newTarget;
        xAngle = 0;
        yAngle = initialAngle;
    }


    private void FaceTarget(Transform target) {
        //get the vector from camera position to the target's position
        Vector3 newDirection = target.position - transform.position;
        //Interpolate from the camera's current direction to the new direction
        newDirection = Vector3.Slerp(transform.forward, newDirection, rotationDamping);
        //look at new direction
        transform.LookAt(transform.position + newDirection);
    }


    //Calculates the vector behind a target given a target, distance, and angle
    public Vector3 CalculateVector(Vector3 direction, float distance, float xAngle, float yAngle) {
        //create backwards vector
        Vector3 result = direction;

        //rotate by angle x and y
        result = Quaternion.Euler(yAngle, -xAngle, 0) * result;

        //scale vector by distance
        if (distance != 0) {
            result = result / result.magnitude * distance;
        }

        return result;
    }



}
