using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [System.Serializable]
    public class PlayerInputKey
    {
        public string buttonName;
        public List<Condition> conditions;
        public PlayerInputAction action;
    }
}