using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [System.Serializable]
    public class InputManager
    {
        public List<InputAxis> axes = new List<InputAxis>();
        private Dictionary<string, InputAxis> axesDict = new Dictionary<string, InputAxis>();
        public List<InputKey> keys = new List<InputKey>();
        private Dictionary<string, InputKey> keyDict = new Dictionary<string, InputKey>();

        [Space(10)]
        public ControllerKeys controllerKeys;

        private RuntimePlatform platform;
        

        // Use this for initialization
        void Start()
        {
            platform = Application.platform;
            ListToDict();
        }

        public void ChangeKey(string keyname, KeyCode key)
        {
            if (keyDict.ContainsKey(keyname)) {
                keyDict[keyname].ChangeKey(key);
            }
        }

        /// <summary>
        /// Returns true if key was pressed
        /// If ExlusiveKeyEvents is true, will not return true if one was pressed while another keybind was held down
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetKeyDown(string name)
        {
            bool result = false;

            if (keyDict.ContainsKey(name)){
                result = keyDict[name].GetKeyDown();
            }

            return result;
        }
        

        /// <summary>
        /// Returns true while one of the keys is being held down
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetKey(string name)
        {
            bool result = false;

            if (keyDict.ContainsKey(name))
            {
                result = keyDict[name].GetKey();
            }

            return result;
        }


        /// <summary>
        /// Returns true when key is released
        /// Variable ExclusiveKeyEvents affects whether to check for both or not
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetKeyUp(string name)
        {
            bool result = false;

            if (keyDict.ContainsKey(name))
            {
                result = keyDict[name].GetKeyUp();
            }

            return result;
        }

        

        /// <summary>
        /// Returns the value of the axis
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public float GetAxis(string name)
        {
            float result = 0;

            if (axesDict.ContainsKey(name)) {
                result = axesDict[name].GetAxis();
            }

            return result;
        }

        //converts the list values to a dict
        private void ListToDict()
        {
            foreach (InputAxis axis in axes) {
                axesDict[axis.axisName] = axis;
            }

            foreach (InputKey key in keys) {
                keyDict[key.keyName] = key;
            }
        }

        public bool IsControllerInput(KeyCode key)
        {
            /*
            switch (input)
            {
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
            }*/

            return controllerKeys.IsControllerKey(key);
        }


    }
}