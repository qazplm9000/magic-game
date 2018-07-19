using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Inputs/Controller Keys List")]
    public class ControllerKeys : ScriptableObject
    {
        public List<KeyCode> keycodes;

        /// <summary>
        /// Returns true if the key is in the list
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsControllerKey(KeyCode key) {
            return keycodes.Contains(key);
        }
    }
}