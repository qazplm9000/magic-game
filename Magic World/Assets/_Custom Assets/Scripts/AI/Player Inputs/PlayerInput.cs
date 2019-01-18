using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Player Input")]
    public class PlayerInput : CharacterInput
    {
        public List<PlayerInputAction> actions;
        public List<PlayerInputKey> inputs;


        public override void Execute(CharacterManager character)
        {
            ExecuteDefaultActions(character);
            ExecutePlayerKeys(character);
        }


        //loops through all events that run regardless of key events
        private void ExecuteDefaultActions(CharacterManager character)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                PlayerInputAction action = actions[i];
                Debug.Log(i);
                if (action != null)
                {
                    action.Execute(character);
                }
            }
        }

        //loops through key events and activate
        private void ExecutePlayerKeys(CharacterManager character)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                PlayerInputKey input = inputs[i];

                if (input != null) {
                    if (World.inputs.GetKeyDown(input.buttonName)) {
                        if (input.conditions.ConditionsPass(character)) {
                            input.action.Execute(character);
                        }
                    }
                }

            }
        }
    }
}