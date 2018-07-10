using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    //private PlayerMovement targetController;

    private Vector3 velocity = Vector3.zero;
    public float targetDistance = 3f;
    public float height = 2f;

    public float smoothTime = 0.3f;

    //used to shift what part of the target to look for (head as opposed to center)
    public Vector3 targetOffset = new Vector3(0, 1.8f, 0);

    public float maxZoom = 10f;
    public float minZoom = 2f;
    public float currentZoom = 3f;

    public float rotateSpeed = 20f;


	// Use this for initialization
	void Start () {
        if (target == null) {
            
        }
        _initCameraPosition();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Move();
        RotateCamera();
	}

    //move the camera with the player
    public void Move()
    {
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, CalculateCameraPosition(), ref velocity, smoothTime);
        transform.position = newPosition;

        TurnTowards();
    }



    //turn towards the target
    public void TurnTowards() {
        transform.LookAt(target.position + target.up*1.8f);
    }



    //rotate camera around target
    public void RotateCamera() {
        if (Input.GetMouseButton(0))
        {
            float rotationAmount = Input.GetAxis("Mouse X");
            transform.RotateAround(target.position + targetOffset, target.up, rotateSpeed * rotationAmount);
        }
    }


    //change camera zoom
    public void ZoomCamera() {

    }


    //Init the position of the camera
    public void _initCameraPosition() {
        float tempDistance = targetDistance * targetDistance;
        tempDistance -= height * height;
        tempDistance = Mathf.Sqrt(tempDistance);

        Vector3 cameraLocation = target.position - (target.forward * tempDistance) + (target.up * height);
        transform.position = cameraLocation;
    }


    //calculates where the camera should be located
    public Vector3 CalculateCameraPosition() {
        //get the vector from the camera to the target without the y axis
        Vector3 directionVector = transform.position - target.position;
        directionVector = directionVector - new Vector3(0, directionVector.y, 0);

        float distance = targetDistance * targetDistance;
        distance -= height * height;
        distance = Mathf.Sqrt(distance);

        directionVector /= directionVector.magnitude;
        directionVector *= distance;
        directionVector += new Vector3(0, height, 0) + target.transform.position;

        return directionVector;

    }



}
