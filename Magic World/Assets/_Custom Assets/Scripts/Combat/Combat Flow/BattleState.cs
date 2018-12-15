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
        public List<CharacterManager> characters;
        public List<CharacterManager> players;
        public List<CharacterManager> enemies;

        //current character's turn
        //public Combatant currentTurn
        public float turnTimer;
        public float turnTime;
        public CharacterManager currentCharacter;


        //public delegate void TurnSwap(Combatant currentCharacter);
        //public event TurnSwap turnSwap;

        // Initialize the battle with given characters
        public void InitBattle()
        {
            //take in arguments for party members and enemies
        }

        // Update is called once per frame
        public void Update()
        {

            if (turnTimer <= 0) {
                turnTimer = 0;
                //wait for current character to finish all actions
                //progress turn when done
                ProgressTurn();
            }


            /* Call while character is taking any action (moving, attacking, casting, item)
            if (currentCharacter.TakingAction()) {
                turnTimer -= Time.deltaTime;
            }*/


        }


        /// <summary>
        /// switch over to next character
        /// </summary>
        public void ProgressTurn() {
            //switch to next character
            //currentCharacter = nextCombatant
            //call turn event
        }


    }
}