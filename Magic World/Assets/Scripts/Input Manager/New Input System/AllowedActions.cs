using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{
    [System.Serializable]
    public class AllowedActions : ISerializationCallbackReceiver
    {
        [SerializeField]
        private bool[] actions;
        public static CharacterAction[] allActions;


        public AllowedActions() {
            InitAllActions();
            InitActions(false);
        }

        public AllowedActions(bool[] allowedActions) {
            InitAllActions();
            InitActions(allowedActions);
        }


        /// <summary>
        /// Checks if a given action is allowed
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ActionIsAllowed(CharacterAction action) {
            int actionIndex = action.GetHashCode();
            //Debug.Log(actionIndex);
            //Debug.Log(allActions.Length);
            return actions[actionIndex];
        }

        public void ChangeBool(CharacterAction action, bool allowed) {
            int actionIndex = action.GetHashCode();
            actions[actionIndex] = allowed;
        }


        /// <summary>
        /// Gets a list of all allowed actions
        /// </summary>
        /// <returns></returns>
        public List<CharacterAction> GetAllowedActions() {
            List<CharacterAction> allowedActions = new List<CharacterAction>(actions.Length);

            for (int i = 0; i < allActions.Length; i++) {
                if (actions[i]) {
                    allowedActions.Add(allActions[i]);
                }
            }

            return allowedActions;
        }


        /// <summary>
        /// Returns the union between this list of bools and another list of bools
        /// </summary>
        /// <param name="otherActions"></param>
        /// <returns></returns>
        public AllowedActions GetUnion(AllowedActions otherActions) {
            bool[] newActions = new bool[actions.Length];

            bool[] otherActionBools = otherActions.GetActionBools();
            for (int i = 0; i < actions.Length; i++) {
                newActions[i] = actions[i] && otherActionBools[i];
            }

            return new AllowedActions(newActions);
        }


        /// <summary>
        /// Gets the list of bools
        /// </summary>
        /// <returns></returns>
        public bool[] GetActionBools() {
            return actions;
        }


        private void InitActions(bool[] allowedActions) {
            actions = new bool[allowedActions.Length];

            for (int i = 0; i < allowedActions.Length; i++) {
                actions[i] = allowedActions[i];
            }
        }

        private void InitActions(bool allowedAction) {
            actions = new bool[allActions.Length];

            for (int i = 0; i < actions.Length; i++) {
                actions[i] = allowedAction;
            }
        }

        /// <summary>
        /// Used to fix the actions array when CharacterAction is changed
        /// </summary>
        private void IncludeNewActions() {
            bool[] newActions = new bool[allActions.Length];

            for (int i = 0; i < allActions.Length; i++) {
                if (i < actions.Length)
                {
                    newActions[i] = actions[i];
                }
                else {
                    newActions[i] = false;
                }
            }
        }

        private void InitAllActions() {
            allActions = (CharacterAction[])Enum.GetValues(typeof(CharacterAction));
        }



        #region ISerializationCallbackReceiver

        public void OnBeforeSerialize()
        {
            if (actions.Length < allActions.Length) {
                IncludeNewActions();
            }
        }

        public void OnAfterDeserialize()
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}