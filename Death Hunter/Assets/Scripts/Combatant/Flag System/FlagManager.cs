using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace StateSystem
{
    public enum Flag
    {
        character_is_moving,
        character_can_move,
        character_is_casting,
        character_can_cast,
        character_is_dodging,
        character_can_dodge,
        character_is_invincible,
        character_is_guarding,
        character_can_guard,
        character_is_grounded,
        character_can_jump
    }

    public class FlagManager : MonoBehaviour, ISerializationCallbackReceiver
    {
        public bool foldout = true;
        public static string[] flagNames;
        public List<bool> flagValues = new List<bool>();


        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public bool GetFlag(Flag flag)
        {
            int index = (int)flag;
            return flagValues[index];
        }

        public void SetFlag(Flag flag, bool value)
        {
            int index = (int)flag;
            bool flagValue = flagValues[index];
            if (flagValue != value)
            {
                flagValues[index] = value;
                Debug.Log($"Flag \"{flag.ToString()}\" set to {value.ToString().ToUpper()}");
            }
        }

        public void DisableFlag(Flag flag)
        {
            SetFlag(flag, false);
        }

        public void EnableFlag(Flag flag)
        {
            SetFlag(flag, true);
        }

        public bool CompareFlag(Flag flag, bool value)
        {
            int index = (int)flag;
            return flagValues[index] == value;
        }



        public void OnAfterDeserialize()
        {

        }

        public void OnBeforeSerialize()
        {
            flagNames = Enum.GetNames(typeof(Flag));
            int numOfFlags = flagNames.Length;
            if (numOfFlags != flagValues.Count)
            {
                flagValues = new List<bool>(numOfFlags);
                bool[] tempFlags = new bool[numOfFlags];
                flagValues.AddRange(tempFlags);
            }
        }
    }
}