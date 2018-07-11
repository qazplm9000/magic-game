using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {

    public KeyCode dodgeButton = KeyCode.JoystickButton4;
    public KeyCode attackButton = KeyCode.JoystickButton2;

    public List<InputCodes> inputs = new List<InputCodes>();
    private Dictionary<string, KeyCode> inputDict = new Dictionary<string, KeyCode>();

    public static ControllerInputManager input;

    [System.Serializable]
    public class InputCodes {
        public string name;
        public KeyCode input;

        public InputCodes(string newName, KeyCode newInput) {
            name = newName;
            input = newInput;
        }
    }

    // Use this for initialization
    void Start () {
        if (input == null)
        {
            input = this;
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
        inputDict[keyname] = key;
        DictToList();
    }

    public bool GetKeyDown(string name) {
        bool result = false;

        if (inputDict.ContainsKey(name))
        {
            result = Input.GetKeyDown(inputDict[name]);
        }

        return result;
    }

    public bool GetKey(string name) {
        return Input.GetKey(inputDict[name]);
    }

    public bool GetKeyUp(string name) {
        return Input.GetKeyUp(inputDict[name]);
    }

    //converts the list values to a dict
    private void ListToDict() {
        inputDict = new Dictionary<string, KeyCode>();

        foreach (InputCodes codes in inputs) {
            inputDict[codes.name] = codes.input;
        }
    }

    //converts the dict value to a list
    private void DictToList() {
        inputs = new List<InputCodes>();

        foreach (string codename in inputDict.Keys) {
            InputCodes code = new InputCodes(codename, inputDict[codename]);
            inputs.Add(code);
        }
    }
}
