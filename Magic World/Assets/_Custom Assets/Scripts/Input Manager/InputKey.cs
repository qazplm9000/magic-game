using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Inputs/Key")]
    public class InputKey : ScriptableObject
    {

        public string keyName;
        public KeyCode desktopKey;
        public KeyCode controllerKey;

        public bool GetKeyDown() {
            return Input.GetKeyDown(desktopKey) || Input.GetKeyDown(controllerKey);
        }

        public bool GetKey() {
            return Input.GetKey(desktopKey) || Input.GetKey(controllerKey);
        }
        
        public bool GetKeyUp() {
            return Input.GetKeyUp(desktopKey) || Input.GetKeyUp(controllerKey);
        }

        public bool ChangeKey(KeyCode code) {
            bool result = false;

            if (InputManager.manager.IsControllerInput(code))
            {
                result = controllerKey != code;
                controllerKey = code;
            }
            else {
                result = desktopKey != code;
                desktopKey = code;
            }

            return result;
        }
    }
}