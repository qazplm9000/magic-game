using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class TurnOrder
    {
        //Number of turns to calculate
        public int numberOfTurns = 10;

        //Position of each character's next turns
        public List<CharacterManager> characters;
        //maybe each character just has their next turn time as a variable in CharacterManager instead
        public List<int> turnTimes;

        public List<CharacterTurn> turns;


        public TurnOrder(List<CharacterManager> characters) {
            this.characters = characters;
            turnTimes = new List<int>();

            for (int i = 0; i < this.characters.Count; i++) {
                //add initial turn time
                //turnTimes.Add(characters.)
            }
        }



        public void CalculateTurns() {
            turns = new List<CharacterTurn>();

            //Start by populating list with just the fastest character
            //Then iterate through the list and add a turn for each
            //Stop loop once all turns slower than last

            
            for (int i = 0; i < numberOfTurns; i++) {
                bool inserted = false;

                for (int j = 0; j < characters.Count; j++) {
                    CharacterManager character = characters[i];
                    int time = character.FutureTurnTime();
                }
            }
        }


        /// <summary>
        /// Inserts the turn if at least one of the following conditions is true:
        ///     1) There are not enough spaces in the list of turns
        ///     2) The character gets a turn faster than the last turn
        /// </summary>
        /// <param name="character"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private bool InsertTurn(CharacterManager character, int time) {
            bool inserted = false;
            CharacterTurn turn = new CharacterTurn(character, time);
            
            for (int i = 0; i < turns.Count; i++) {

                if (time < turns[i].turnTime)
                {
                    turns.Insert(i, turn);
                    inserted = true;
                    break;
                }
                else if (time == turns[i].turnTime) {
                    int characterAgility = character.GetAgility();
                    int comparisonAgility = turns[i].character.GetAgility();

                    if (characterAgility > comparisonAgility) {
                        turns.Insert(i, turn);
                        inserted = true;
                        break;
                    }
                }
            }

            
            if (inserted)
            {
                if (turns.Count > numberOfTurns)
                {
                    turns.RemoveAt(numberOfTurns);
                }
            }
            else { 
                if (turns.Count < numberOfTurns) {
                    turns.Add(turn);
                    inserted = true;
                }
            }


            return inserted;
        }
    }
}