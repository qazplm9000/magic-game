using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            if (Input.GetAxis("Mouse X") != 0 ||
                Input.GetAxis("Mouse Y") != 0) {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
