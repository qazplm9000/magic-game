using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatSystem
{
    [System.Serializable]
    public class TurnOrder
    {
        private List<CharacterManager> characters;

        private List<CharacterManager> listOfTurns = new List<CharacterManager>();
        private int numberOfTurns = 5;


        public TurnOrder(List<CharacterManager> newCharacters) {
            characters = newCharacters;
            SetTurnOrder(numberOfTurns);
        }

        public TurnOrder(List<CharacterManager> newCharacters, int newNumberOfTurns) {
            characters = newCharacters;
            numberOfTurns = newNumberOfTurns;
            SetTurnOrder(numberOfTurns);
        }



        public CharacterManager GetCurrentTurn() {
            return listOfTurns[0];
        }

        public void UpdateTurnOrder() {
            SetTurnOrder(numberOfTurns);
        }


        private void SetTurnOrder(int turns = 5) {
            listOfTurns = new List<CharacterManager>(turns);

            List<int> turnCounter = new List<int>(characters.Count);
            for (int i = 0; i < characters.Count; i++) {
                turnCounter.Add(0);
            }


            
            for (int i = 0; i < turns; i++) {
                CharacterManager nextTurn = characters[0];
                float nextTurnTime = characters[0].GetTurnTime(turnCounter[0]);
                int turnIndex = 0;

                for (int j = 1; j < characters.Count; j++) {
                    CharacterManager tempTurn = characters[j];
                    int tempTurnCount = turnCounter[j];
                    float tempTime = characters[j].GetTurnTime(tempTurnCount);

                    if (nextTurnTime > tempTime) {
                        nextTurn = tempTurn;
                        nextTurnTime = tempTime;
                        turnIndex = j;
                    }
                }

                listOfTurns.Add(nextTurn);
                turnCounter[turnIndex] = turnCounter[turnIndex] + 1;
            }
        }


    }
}
