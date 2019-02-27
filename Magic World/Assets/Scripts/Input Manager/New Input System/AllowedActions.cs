using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{
    [CreateAssetMenu(menuName = "Inputs/Actions Allowed")]
    public class AllowedActions : ScriptableObject
    {
        [System.Serializable]
        public class ActionBools {
            public PlayerInput2 action;
            public bool allowed;
        }

        public List<ActionBools> actions;
        private Dictionary<PlayerInput2, bool> actionsDict;


        public bool ActionIsAllowed(PlayerInput2 input) {
            bool allowed = false;

            if (actionsDict.ContainsKey(input)) {
                allowed = actionsDict[input];
            }

            return allowed;
        }



        public void OnEnable()
        {
            InitDict();
        }

        public void OnDisable()
        {
            
        }

        private void InitDict() {
            actionsDict = new Dictionary<PlayerInput2, bool>();

            int index = 0;
            while (index < actions.Count) {
                ActionBools currentAction = actions[index];

                if (!actionsDict.ContainsKey(currentAction.action))
                {
                    actionsDict[currentAction.action] = currentAction.allowed;
                    index++;
                }
                else {
                    actions.RemoveAt(index);
                }
            }
        }
    }
}