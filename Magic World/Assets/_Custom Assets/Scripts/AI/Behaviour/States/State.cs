using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Behaviour
{
    [CreateAssetMenu(menuName = "State/State")]
    public class State : ScriptableObject
    {
        public List<Transition> transitions = new List<Transition>();

        public void Tick() {

        }

        public Transition AddTransition() {
            Transition result = new Transition();
            transitions.Add(result);
            return result;
        }

    }
}