using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [System.Serializable]
    public class PlayerInputKey
    {
        public string buttonName;
        public ConditionList conditions;
        public PlayerAction action;
    }
}