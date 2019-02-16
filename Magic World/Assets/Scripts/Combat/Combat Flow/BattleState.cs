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

        public Element currentElement = Element.None;
        public int elementalCharge = 0;

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
                currentCharacter.TakeTurn();
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


        public void ApplyCharge(Element element, int charge = 1) {
            if (element != Element.None) {
                if (element == currentElement)
                {
                    elementalCharge += charge;
                }
                else {
                    elementalCharge -= charge;
                }

                //changes the current element if charge becomes 0 or lower
                if (elementalCharge <= 0) {
                    currentElement = element;
                    elementalCharge = -elementalCharge + 1;
                }
            }
        }


        public void SetCharacters(CharacterManager[] allCharacters) {
            characters = new List<CharacterManager>();
            enemies = new List<CharacterManager>();
            players = new List<CharacterManager>();

            for (int i = 0; i < allCharacters.Length; i++) {
                CharacterManager character = allCharacters[i];

                characters.Add(character);

                if (character.tag == "enemy")
                {
                    enemies.Add(character);
                }
                else {
                    players.Add(character);
                }
            }
        }


    }
}