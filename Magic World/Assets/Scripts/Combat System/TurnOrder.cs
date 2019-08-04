using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatSystem
{
    [System.Serializable]
    public class TurnOrder
    {
        private List<Combatant> characters;

        private List<Combatant> listOfTurns = new List<Combatant>();
        private int numberOfTurns = 5;


        public TurnOrder(List<Combatant> newCharacters) {
            characters = newCharacters;
            SetTurnOrder(numberOfTurns);
        }

        public TurnOrder(List<Combatant> newCharacters, int newNumberOfTurns) {
            characters = newCharacters;
            numberOfTurns = newNumberOfTurns;
            SetTurnOrder(numberOfTurns);
        }



        public Combatant GetCurrentTurn() {
            return listOfTurns[0];
        }

        public void UpdateTurnOrder() {
            SetTurnOrder(numberOfTurns);
        }


        private void SetTurnOrder(int turns = 5) {
            listOfTurns = new List<Combatant>(turns);

            List<int> turnCounter = new List<int>(characters.Count);
            for (int i = 0; i < characters.Count; i++) {
                turnCounter.Add(0);
            }


            
            for (int i = 0; i < turns; i++) {
                Combatant nextTurn = characters[0];
                float nextTurnTime = characters[0].GetTurnTime(turnCounter[0]);
                int turnIndex = 0;

                for (int j = 1; j < characters.Count; j++) {
                    Combatant tempTurn = characters[j];
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
