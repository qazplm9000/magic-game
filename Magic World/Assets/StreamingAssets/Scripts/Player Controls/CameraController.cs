using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform player;
    //private TownController controller;

    private Vector3 pivot_point;

    public Vector3 cameraDistance;

    //public float back_offset = 0.1f;
    public float up_offset = 0.1f;

    public float zoomSpeed = 5.0f;
    private float currentZoom = 4f;

    public float pitch = 1.8f;

    private float yawX = 0f;
    private float yawY = 0f;
    public float yawSpeed = 50f;

    public float clampBottom = -70f;
    public float clampTop = 70f;
    //public string test;

	// Use this for initialization
	void Start () {
        //controller = player.GetComponent<TownController>();

        if (clampTop < clampBottom) {
            throw new System.Exception();
        }

        pivot_point = player.position + player.up * up_offset;

        transform.position = (pivot_point) - player.forward * currentZoom;
        transform.LookAt(player.position + Vector3.up * pitch);

        cameraDistance = pivot_point - transform.position;
    }

    void Update() {
        //transform.position = pivot_point - cameraDistance;
        pivot_point = player.position + player.up * up_offset;
        transform.position = pivot_point - cameraDistance;

        RotateCamera();

        cameraDistance = pivot_point - transform.position;
    }

	// Update is called once per frame
	void LateUpdate () {

        
    
    }

    



    public void Move()
    {
        
    }

    public void RotateCamera() {
        if (Input.GetMouseButton(0))
        {
            yawX = Input.GetAxis("Mouse X") * yawSpeed / 5;
            yawY = Input.GetAxis("Mouse Y") * -yawSpeed / 5;

            //Debug.Log(yawY);

            float tempAngle = transform.localRotation.eulerAngles.x;

            if (tempAngle > 180)
            {
                tempAngle -= 360;
            }

            if (yawY + tempAngle > clampTop)
            {
                yawY = clampTop - tempAngle;
            }
            if (yawY + tempAngle < clampBottom)
            {
                yawY = clampBottom - tempAngle;
            }

            Vector3 distanceVector = pivot_point - transform.position;
            Vector3 horizontalVector = new Vector3(distanceVector.z, 0, -distanceVector.x);
            horizontalVector /= horizontalVector.magnitude;

            transform.RotateAround(pivot_point, horizontalVector, yawY);
            transform.RotateAround(pivot_point, player.up, yawX);
        }
        if (Input.GetMouseButtonUp(0))
        {
            yawX = 0;
            yawY = 0;
        }

        currentZoom = Mathf.Clamp(currentZoom - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, 1, 7);

        transform.LookAt(player.position + Vector3.up * pitch);
    }


    


}
