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
        

        
        //plays on the frame the state enters
        public List<StateEventObject> enterEvents;
        //plays on the frame the state exits
        public List<StateEventObject> exitEvents;
        //plays every frame while in the state
        public List<StateEventObject> updateEvents;

        public CharacterInput inputs;


        public void EnterState(CharacterManager character) {
            for (int i = 0; i < enterEvents.Count; i++) {
                if (enterEvents[i] != null) {
                    enterEvents[i].Execute(character);
                }
            }
        }

        public void Execute(CharacterManager character) {
            if (inputs != null) {
                inputs.Execute(character);
            }
        }


        public void ExitState(CharacterManager character) {
            for (int i = 0; i < exitEvents.Count; i++) {
                if (exitEvents[i] != null) {
                    exitEvents[i].Execute(character);
                }
            }
        }
    }
}