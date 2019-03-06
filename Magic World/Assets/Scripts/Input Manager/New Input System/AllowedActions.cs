using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{
    [System.Serializable]
    public class AllowedActions
    {
        [System.Serializable]
        public class ActionBools {
            public PlayerInput2 action;
            public bool allowed;
        }

        public List<ActionBools> actions;
        
        private Dictionary<PlayerInput2, bool> actionsDict;


        public AllowedActions() {
            InitDict();
        }


        public bool ActionIsAllowed(PlayerInput2 input) {
            bool allowed = false;

            if (actionsDict.ContainsKey(input)) {
                allowed = actionsDict[input];
            }

            return allowed;
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