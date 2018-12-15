using System.Collections;
using System.Collections.Generic;
using CombatSystem;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Player Battle Inputs")]
    public class PlayerBattleInput : CharacterBattleInput
    {
        public List<PlayerInputAction> actions;
        public List<PlayerInputKey> inputs;

        public override void Execute(BattleState battle, CharacterManager character)
        {
            ExecuteDefaultActions(character);
            ExecutePlayerKeys(character);
        }

        
        //loops through all events that run regardless of key events
        private void ExecuteDefaultActions(CharacterManager character) {
            for (int i = 0; i < actions.Count; i++) {
                PlayerInputAction action = actions[i];

                if (action != null)
                {
                    action.Execute(character);
                }
            }
        }

        //loops through all key down events
        private void ExecutePlayerKeys(CharacterManager character) {
            for (int i = 0; i < inputs.Count; i++)
            {
                PlayerInputKey input = inputs[i];

                //ignore if button is not pressed down
                if (!World.inputs.GetKeyDown(input.buttonName))
                {
                    continue;
                }

                //ignore if any condition does not hold true
                if (!ConditionsPass(input.conditions, character)) {
                    continue;
                }

                //perform action
                if (input.action != null)
                {
                    input.action.Execute(character);
                }

            }
        }

        //returns true if all conditions pass
        private bool ConditionsPass(List<Condition> conditions, CharacterManager character) {
            bool result = true;

            for (int i = 0; i < conditions.Count; i++) {
                if (!conditions[i].Execute(character)) {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}