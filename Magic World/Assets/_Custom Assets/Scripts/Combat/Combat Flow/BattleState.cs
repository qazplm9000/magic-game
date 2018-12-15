using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Handles the combat logic
namespace CombatSystem
{
    [System.Serializable]
    public class BattleState
    {

        //list of all characters
        //public List<Combatant> combatants
        //public List<Combatant> players
        //public List<Combatant> enemies

        //current character's turn
        //public Combatant currentTurn
        public float turnTimer;
        public float turnTime;
        public bool actionTaken = false;


        //public delegate void TurnSwap(Combatant currentCharacter);
        //public event TurnSwap turnSwap;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

            if (actionTaken) {
                turnTimer -= Time.deltaTime;

                if (turnTimer < turnTime) {
                    ProgressTurn();
                }
            }

            Debug.Log("Test");


        }



        public void ProgressTurn() {
            actionTaken = false;
            //switch to next character
            //currentCharacter = nextCombatant
            //call turn event
        }


    }
}