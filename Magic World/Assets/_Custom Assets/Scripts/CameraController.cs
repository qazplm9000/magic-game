using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    //private PlayerMovement targetController;

    //used to shift what part of the target to look for (head as opposed to center)
    public Vector3 targetOffset = new Vector3(0, 1.8f, 0);

    public float maxZoom = 10f;
    public float minZoom = 2f;
    public float currentZoom = 3f;

    public float rotateSpeed = 20f;

    public delegate void OnDirectionChange();
    public event OnDirectionChange ChangeDirection;

	// Use this for initialization
	void Start () {
        if (target == null) {
            
        }
        //targetController = target.GetComponent<PlayerMovement>();
        //ChangeDirection += targetController.UpdateForwardDirection;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Move();
        RotateCamera();
        ChangeDirection();
	}

    //move the camera with the player
    public void Move()
    {
        transform.position = target.position + targetOffset + (transform.forward * -1 * currentZoom);
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


}
