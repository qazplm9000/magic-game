using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {

    public KeyCode dodgeButton = KeyCode.JoystickButton4;
    public KeyCode attackButton = KeyCode.JoystickButton2;

    public static ControllerInputManager controllerInput;



    // Use this for initialization
    void Start () {
        if (controllerInput == null)
        {
            controllerInput = this;
        }
        else {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
