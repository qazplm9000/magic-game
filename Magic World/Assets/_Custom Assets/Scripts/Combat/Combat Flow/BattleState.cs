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
        private int characterIndex = 0;


        //public delegate void TurnSwap(Combatant currentCharacter);
        //public event TurnSwap turnSwap;

        // Initialize the battle with given characters
        public void InitBattle()
        {
            //take in arguments for party members and enemies
            //determine turn order
        }

        // Update is called once per frame
        public void Update()
        {

            if (turnTimer <= 0)
            {
                turnTimer = 0;
                //wait for current character to finish all actions
                //progress turn when done
                ProgressTurn();
            }
            else {
                //currentCharacter.defaultState.Execute(currentCharacter);
                if (currentCharacter.playerController != null)
                {
                    currentCharacter.TakeTurn();
                }
                turnTimer -= Time.deltaTime;
            }


        }


        /// <summary>
        /// switch over to next character
        /// </summary>
        public void ProgressTurn() {
            //set turn timer
            turnTimer = turnTime;

            //swap to next character
            characterIndex++;
            characterIndex %= characters.Count;
            currentCharacter = characters[characterIndex];
        }


    }
}