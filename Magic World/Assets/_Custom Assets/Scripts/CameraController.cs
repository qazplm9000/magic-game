using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

public class CameraController : MonoBehaviour {

    public Transform target;
    private CharacterManager targetManager {
        get { return _targetManager; }
        set {
            _targetManager = value;
            targetPoint = value.GetComponentInChildren<TargetPoint>();
            playerTarget = value.target;
        }
    }
    private CharacterManager _targetManager;
    public TargetPoint targetPoint;
    public TargetPoint playerTarget;
    
    //private PlayerMovement targetController;

    private Vector3 velocity = Vector3.zero;
    public float targetDistance = 3f;
    public float height = 0f;
    private GameObject cameraMarker;
    private Vector3 distanceVector;

    public float smoothTime = 0.3f;

    //used to shift what part of the target to look for (head as opposed to center)
    public Vector3 targetOffset = new Vector3(0, 1.8f, 0);

    public float maxZoom = 10f;
    public float minZoom = 2f;
    public float zoomSpeed = 2f;


    public float minAngle = -60;
    public float maxAngle = 60f;
    public float rotateSpeed = 20f;

    public float minTurn = -20;
    public float maxTurn = 20;

	// Use this for initialization
	void Start () {
        if (target == null) {
            
        }
        targetManager = target.GetComponent<CharacterManager>();

        //creates a new empty game object as the camera's position marker
        cameraMarker = new GameObject();
        _initCameraPosition();
	}

    private void Update()
    {
        playerTarget = targetManager.target;
    }

    
    void LateUpdate () {
        if (playerTarget == null)
        {
            MoveCameraMarker(targetPoint.transform.position);
            Move();
            TurnTowards(targetPoint.transform.position);

            RotateCamera();

            ZoomCamera(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);
        }
        else {
            MoveCameraMarker(targetPoint.transform.position);
            Move();
            TurnTowards((targetPoint.transform.position + playerTarget.transform.position) / 2);

            RotateCamera();

            ZoomCamera(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);
        }
	}

    //move the camera with the player
    public void Move()
    {
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, cameraMarker.transform.position, ref velocity, smoothTime);
        transform.position = newPosition;
    }



    //turn towards the target
    public void TurnTowards(Vector3 lookPoint) {
        Vector3 targetPosition = Vector3.zero;

        if (playerTarget == null)
        {
            targetPosition = target.position + target.up * 1.8f;
        }
        else {
            targetPosition = target.position + target.up * 1.8f;
            //targetPosition += playerTarget.transform.position;
            //targetPosition /= 2;
        }

        transform.LookAt(lookPoint);
    }



    //rotate camera around target
    public void RotateCamera(bool clampX = false) {

        float xAngle = InputManager.manager.GetAxis("Horizontal Right") * Time.deltaTime * rotateSpeed;
        float yAngle = InputManager.manager.GetAxis("Vertical Right") * Time.deltaTime * rotateSpeed;

        Vector3 perpendicular = new Vector3(-distanceVector.z, 0, distanceVector.x);

        float angle = Vector3.SignedAngle(distanceVector, 
                                            new Vector3(distanceVector.x, 0, distanceVector.z), 
                                            perpendicular) + yAngle;

        //Debug.DrawLine(target.transform.position + targetOffset, distanceVector, Color.blue);
        //Debug.DrawLine(target.transform.position + targetOffset, new Vector3(distanceVector.x, 0, distanceVector.z), Color.green);
        //Debug.DrawLine(target.transform.position + targetOffset, perpendicular, Color.red);
        /*if (angle > maxAngle)
        {
            yAngle += angle - maxAngle;
        }
        else if (angle < minAngle) {
            yAngle -= minAngle - angle;
        }*/

        angle = GetClampedAngle(yAngle, minAngle, maxAngle, new Vector3(distanceVector.x, 0, distanceVector.z), perpendicular);

        //Debug.Log(angle);

        if (!clampX)
        {
            distanceVector = Quaternion.Euler(0, xAngle, 0) * distanceVector;
            Vector3 temp = perpendicular / perpendicular.magnitude * yAngle;
            distanceVector = Quaternion.Euler(temp.x, 0, temp.z) * distanceVector;
        }
        else {

            Vector3 temp = perpendicular / perpendicular.magnitude * yAngle;
        }

    }


    private float GetClampedAngle(float deltaAngle, float minAngle, float maxAngle, Vector3 distanceAxis, Vector3 rotateAxis) {
        float result = Vector3.SignedAngle(distanceVector, distanceAxis, rotateAxis) + deltaAngle;

        if (result < minAngle)
        {
            result = minAngle - result;
        }
        else if (result > maxAngle)
        {
            result = result - maxAngle;
        }
        else {
            result = deltaAngle;
        }

        return result;
    }


    //change camera zoom
    public void ZoomCamera(float zoom) {
        targetDistance += zoom;

        if (targetDistance > maxZoom)
        {
            targetDistance = maxZoom;
        }
        else if (targetDistance < minZoom) {
            targetDistance = minZoom;
        }

        float ratio = targetDistance / distanceVector.magnitude;

        distanceVector *= ratio;
    }




    //Init the position of the camera and the marker
    public void _initCameraPosition() {

        float tempDistance = targetDistance;

        height = height > targetDistance ? targetDistance : height;
        tempDistance = Mathf.Sqrt(targetDistance * targetDistance - height * height);

        //sets the camera's initial starting location
        Vector3 cameraLocation = (target.transform.position + targetOffset) + (height*target.transform.up) - (target.forward * tempDistance);
        transform.position = cameraLocation;

        //sets the camera marker to the designated position behind the character
        cameraMarker.transform.position = cameraLocation;
        distanceVector = (target.position + targetOffset) - cameraMarker.transform.position;
    }


    //calculates where the camera should be located
    public void MoveCameraMarker(Vector3 targetLocation) {
        cameraMarker.transform.position = targetLocation - distanceVector;
    }

    /*
    public void RecalculateDistanceVector() {
        distanceVector = (target.position + targetOffset) - cameraMarker.transform.position;
        
    }*/
    


    


}
