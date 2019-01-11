using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

namespace CharacterStateSystem
{
    [CreateAssetMenu(menuName = "State/Character State")]
    public class CharacterState : ScriptableObject
    {
        //name for the state
        //will be used as the key in the state tree dictionary
        public string StateName;

        //will go to exit node upon hitting exit condition
        //exits to default if left blank
        public string ExitNode;

        
        //plays on the frame the state enters
        public List<StateEventObject> enterEvents;
        //plays on the frame the state exits
        public List<StateEventObject> exitEvents;
        //plays every frame while in the state
        public List<StateEventObject> updateEvents;

        public List<PlayerInputKey> keys;


        public void Execute(CharacterManager character) {
            for (int i = 0; i < keys.Count; i++) {
                PlayerInputKey key = keys[i];
                if (World.inputs.GetKeyDown(key.buttonName) && key.conditions.ConditionsPass(character)) {
                    key.action.Execute(character);
                }
            }
        }

    }
}