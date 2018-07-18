using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    //Checks whether keys should be checked exclusively
    public bool ExclusiveKeyEvents = false;

    public List<InputCodes> inputs = new List<InputCodes>();
    private Dictionary<string, InputCodes> inputDict = new Dictionary<string, InputCodes>();

    public bool invertKeyboardXAxis = false;
    public bool invertKeyboardYAxis = false;
    public bool invertMouseXAxis = false;
    public bool invertMouseYAxis = false;
    
    public bool invertLeftStickXAxis = false;
    public bool invertLeftStickYAxis = false;
    public bool invertRightStickXAxis = false;
    public bool invertRightStickYAxis = false;

    private RuntimePlatform platform;
    

    public static InputManager manager;

    [System.Serializable]
    public class InputCodes {
        public string name;
        public KeyCode controllerInput;
        public KeyCode computerInput;

        public InputCodes(string newName, KeyCode newInput) {
            name = newName;
            if (InputManager.KeyIsControllerInput(newInput))
            {
                controllerInput = newInput;
            }
            else {
                computerInput = newInput;
            }
        }

        public InputCodes(string newName, KeyCode controllerInput, KeyCode computerInput) {
            name = newName;
            if (InputManager.KeyIsControllerInput(controllerInput))
            {
                this.controllerInput = controllerInput;
            }
            else {

            }
            this.computerInput = computerInput;
        }
    }

    // Use this for initialization
    void Start () {
        if (manager == null)
        {
            manager = this;
            platform = Application.platform;
        }
        else {
            Destroy(this);
        }

        ListToDict();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeKey(string keyname, KeyCode key) {
        if (KeyIsControllerInput(key))
        {
            inputDict[keyname].controllerInput = key;
        }
        else {
            inputDict[keyname].computerInput = key;
        }
        DictToList();
    }

    /// <summary>
    /// Returns true if key was pressed
    /// If ExlusiveKeyEvents is true, will not return true if one was pressed while another keybind was held down
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool GetKeyDown(string name) {
        bool result = false;

        if (ExclusiveKeyEvents)
        {
            result = GetKeyDownExclusive(name);
        }
        else {
            result = GetKeyDownNotExclusive(name);
        }

        return result;
    }

    /// <summary>
    /// Returns true when button is pressed on controller or keyboard
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool GetKeyDownNotExclusive(string name) {
        bool result = false;

        try
        {
            result = Input.GetKeyDown(inputDict[name].controllerInput);
        }
        catch { }

        try
        {
            result = result || Input.GetKeyDown(inputDict[name].computerInput);
        }
        catch { }

        return result;
    }


    /// <summary>
    /// Returns true only if one is pressed down while the other isn't or if both are pressed at the exact same time
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool GetKeyDownExclusive(string name) {
        bool result = false;
        bool onControllerDown = false;
        bool onComputerDown = false;
        bool controllerDown = false;
        bool computerDown = false;

        if (inputDict.ContainsKey(name))
        {
            //check if controller input is true
            try
            {
                onControllerDown = Input.GetKeyDown(inputDict[name].controllerInput);
                controllerDown = Input.GetKey(inputDict[name].controllerInput);
            }
            catch { }

            //check if computer input is true
            try
            {
                onComputerDown = Input.GetKeyDown(inputDict[name].computerInput);
                computerDown = Input.GetKey(inputDict[name].computerInput);
            }
            catch { }

            //make sure other keybind was not pressed
            result = (onControllerDown || onComputerDown) &&
                        (controllerDown ^ computerDown);

            //override to true if both were pressed down at same time
            if (onControllerDown && onComputerDown) {
                result = true;
            }
        }

        return result;
    }

    /// <summary>
    /// Returns true while one of the keys is being held down
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool GetKey(string name) {
        return Input.GetKey(inputDict[name].controllerInput) || Input.GetKey(inputDict[name].computerInput);
    }


    /// <summary>
    /// Returns true when key is released
    /// Variable ExclusiveKeyEvents affects whether to check for both or not
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool GetKeyUp(string name) {
        bool result = false;

        if (ExclusiveKeyEvents)
        {
            result = GetKeyUpExclusive(name);
        }
        else {
            result = GetKeyUpNotExclusive(name);
        }

        return result;
    }

    /// <summary>
    /// Returns true when one of the keys is released
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool GetKeyUpNotExclusive(string name) {
        bool result = false;

        try
        {
            result = Input.GetKeyUp(inputDict[name].controllerInput);
        }
        catch { }

        try
        {
            result = result || Input.GetKeyUp(inputDict[name].computerInput);
        }
        catch { }

        return result;
    }
    
    /// <summary>
    /// Returns true upon releasing all buttons
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool GetKeyUpExclusive(string name) {
        bool result = false;
        bool onControllerUp = false;
        bool onComputerUp = false;
        bool controllerDown = false;
        bool computerDown = false;

        if (inputDict.ContainsKey(name))
        {
            //check if controller input is true
            try
            {
                onControllerUp = Input.GetKeyUp(inputDict[name].controllerInput);
                controllerDown = Input.GetKey(inputDict[name].controllerInput);
            }
            catch { }

            //check if computer input is true
            try
            {
                onComputerUp = Input.GetKeyUp(inputDict[name].controllerInput);
                computerDown = Input.GetKey(inputDict[name].controllerInput);
            }
            catch { }

            //make sure neither keybind is still being held down
            result = (onControllerUp || onComputerUp) &&
                        (!controllerDown && !computerDown);
        }

        return result;
    }

    /// <summary>
    /// Returns the value of the axis
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public float GetAxis(string name) {
        float result = 0;

        switch (name.ToLower()) {
            case "horizontal":
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Controller Horizontal")))
                {
                    //computer
                    result = Input.GetAxis("Horizontal");
                    result = invertKeyboardXAxis ? -result : result;
                }
                else {
                    //controller
                    result = Input.GetAxis("Controller Horizontal");
                    result = invertLeftStickXAxis ? -result : result;
                }
                break;
            case "vertical":
                if (Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(Input.GetAxis("Controller Vertical")))
                {
                    //computer
                    result = Input.GetAxis("Vertical");
                    result = invertKeyboardYAxis ? -result : result;
                }
                else
                {
                    //controller
                    result = Input.GetAxis("Controller Vertical");
                    result = invertLeftStickYAxis ? -result : result;
                }
                break;
            case "mouse x":
                if (Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Abs(Input.GetAxis("Controller X")))
                {
                    //computer
                    result = Input.GetAxis("Mouse X");
                    result = invertMouseXAxis ? -result : result;
                }
                else
                {
                    //controller
                    result = Input.GetAxis("Controller X");
                    result = invertRightStickXAxis ? -result : result;
                }
                break;
            case "mouse y":
                if (Mathf.Abs(Input.GetAxis("Mouse Y")) > Mathf.Abs(Input.GetAxis("Controller Y")))
                {
                    //computer
                    result = Input.GetAxis("Mouse Y");
                    result = invertMouseYAxis ? -result : result;
                }
                else
                {
                    //controller
                    result = Input.GetAxis("Controller Y");
                    result = invertRightStickXAxis ? -result : result;
                }
                break;
        }

        return result;
    }

    //converts the list values to a dict
    private void ListToDict() {
        inputDict = new Dictionary<string, InputCodes>();

        foreach (InputCodes codes in inputs) {
            inputDict[codes.name] = codes;
        }
    }

    //converts the dict value to a list
    private void DictToList() {
        inputs = new List<InputCodes>();

        foreach (string codename in inputDict.Keys) {
            InputCodes code = inputDict[codename];
            inputs.Add(code);
        }
    }

    public static bool KeyIsControllerInput(KeyCode input) {
        bool result = false;

        switch (input) {
            case KeyCode.JoystickButton0:
            case KeyCode.JoystickButton1:
            case KeyCode.JoystickButton2:
            case KeyCode.JoystickButton3:
            case KeyCode.JoystickButton4:
            case KeyCode.JoystickButton5:
            case KeyCode.JoystickButton6:
            case KeyCode.JoystickButton7:
            case KeyCode.JoystickButton8:
            case KeyCode.JoystickButton9:
            case KeyCode.JoystickButton10:
            case KeyCode.JoystickButton11:
            case KeyCode.JoystickButton12:
            case KeyCode.JoystickButton13:
            case KeyCode.JoystickButton14:
            case KeyCode.JoystickButton15:
            case KeyCode.JoystickButton16:
            case KeyCode.JoystickButton17:
            case KeyCode.JoystickButton18:
            case KeyCode.JoystickButton19:
                result = true;
                break;
        }

        return result;
    }


}
