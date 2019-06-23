using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;
using PartySystem;
using System;

namespace BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        BattleContext battleContext;

        public List<CharacterManager> allCharacters;
        public List<CharacterManager> allEnemies;
        public List<CharacterManager> allAllies;

        public Party enemyParty;
        public Party playerParty;
        public Party neutralParty;

        public List<CharacterTurn> turnOrder;
        private List<CharacterTurn> allCharactersNextTurns;

        public CharacterManager currentTurn;
        public int turnIndex = -1;
        public float turnTime = 0;
        public float turnTimer = 0;


        [Header("Field Element")]
        public bool lockFieldElement = false;
        public AbilityElement fieldElement;
        public AbilityElement nullElement;

        [SerializeField]
        [Tooltip("Number of turns to calculate in turnOrder")]
        private int numberOfTurns = 10;



        private void Awake()
        {
            InitCharacter();
            InitFirstTurns();
            CalculateTurnOrder();
            InitTurn(turnOrder[0]);
        }

        private void Start()
        {
            
        }


        public void Update()
        {
            turnTimer -= Time.deltaTime;

            
            if (turnTimer <= 0)
            {
                //if (currentTurn.NotTakingAction())
                //{
                    ChangeTurn();
                //}
            }


            for (int i = 0; i < allCharacters.Count; i++)
            {
                CharacterManager character = allCharacters[i];

                if (character == currentTurn)
                {
                    character.TakeTurn(battleContext);
                }
                else if (IsTargetted(character))
                {
                    character.Defend(battleContext);
                }
                else {
                    //run when either not enemy, or not being targetted
                    character.Idle(battleContext);
                }
            }
        }


        private void InitTurn(CharacterTurn nextTurn) {
            currentTurn = nextTurn.character;
            turnTime = currentTurn.GetAttackTime();
            turnTimer = turnTime;

            //currentTurn.OnTurnStart();
        }


        public void ChangeTurn()
        {
            turnOrder[0].turnTime = IncrementTurn(turnOrder[0]).turnTime;
            CalculateTurnOrder();

            InitTurn(turnOrder[0]);
        }


        private void InitCharacter()
        {
            CharacterManager[] characters = FindObjectsOfType<CharacterManager>();
            allCharacters = new List<CharacterManager>();
            allEnemies = new List<CharacterManager>();
            allAllies = new List<CharacterManager>();

            for (int i = 0; i < characters.Length; i++)
            {
                allCharacters.Add(characters[i]);

                if (characters[i].tag == "enemy")
                {
                    allEnemies.Add(characters[i]);
                }
                else
                {
                    allAllies.Add(characters[i]);
                }
            }
        }




        /*******************************************/
        /*******************************************/
        /************                  *************/
        /***********  Element Functions ************/
        /*************                 *************/
        /*******************************************/
        /*******************************************/


        public void ChangeFieldElement(AbilityElement newElement)
        {
            if (newElement != nullElement)
            {
                fieldElement = newElement;
            }
        }

        public void ResetElement()
        {
            fieldElement = nullElement;
        }



        public AbilityElement GetFieldElement() {
            return fieldElement;
        }







        /**********************************************/
        /**********************************************/
        /************                     *************/
        /***********  Turn Order Functions ************/
        /*************                    *************/
        /**********************************************/
        /**********************************************/



        /// <summary>
        /// Initializes the first turns for all characters
        /// </summary>
        private void InitFirstTurns() {
            allCharactersNextTurns = new List<CharacterTurn>();

            for (int i = 0; i < allCharacters.Count; i++) {
                CharacterTurn nextTurn = new CharacterTurn(allCharacters[i], CalculateInitiative(allCharacters[i]));
                allCharactersNextTurns.Add(nextTurn);
            }
        }



        /// <summary>
        /// Calculates the turn order
        /// </summary>
        public void CalculateTurnOrder()
        {
            turnOrder = new List<CharacterTurn>();

            for (int i = 0; i < allCharactersNextTurns.Count; i++) {
                CharacterTurn nextTurn = allCharactersNextTurns[i];

                //On the first iteration, just populate the list as if the first character is the only one taking turns
                if (i == 0)
                {
                    turnOrder.Add(nextTurn);

                    while (turnOrder.Count < numberOfTurns)
                    {
                        nextTurn = IncrementTurn(nextTurn);
                        turnOrder.Add(nextTurn);
                    }
                }
                else { //On other iterations, loop through and insert turn if faster
                    for (int j = 0; j < numberOfTurns; j++) {
                        if (nextTurn.turnTime < turnOrder[j].turnTime)
                        {
                            turnOrder.Insert(j, nextTurn);
                            nextTurn = IncrementTurn(nextTurn);
                            turnOrder.RemoveAt(numberOfTurns);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Calculates the following turn for the character
        /// </summary>
        /// <param name="turn"></param>
        /// <returns></returns>
        private CharacterTurn IncrementTurn(CharacterTurn turn) {
            CharacterManager character = turn.character;
            int nextTurnTime = character.CalculateNextTurn(turn.turnTime);

            return new CharacterTurn(character, nextTurnTime);
        }



        /// <summary>
        /// Adds delay to the character
        /// </summary>
        /// <param name="character"></param>
        /// <param name="delay"></param>
        public void AddDelay(CharacterManager character, int delay) {
            for (int i = 0; i < allCharactersNextTurns.Count; i++){
                CharacterTurn nextTurn = allCharactersNextTurns[i];

                if (nextTurn.character == character) {
                    nextTurn.turnTime += delay;
                    CalculateTurnOrder();
                    break;
                }
            }
        }



        /// <summary>
        /// Called to completely reset the battle manager's data after a battle ends
        /// </summary>
        public void ResetBattleManager() {

        }



        private int CalculateInitiative(CharacterManager character) {
            return 0;
        }

        private int CalculateNextTurn(CharacterManager character, int currentTurn) {
            return currentTurn + 1000 / character.GetAgility();
        }


        //Likely take in character, target type (friend, enemy, etc), radius, etc
        public void Target() {

        }

        public bool IsTargetted(CharacterManager target) {
            return battleContext.IsTargetted(target);
        }

        public bool IsEnemy(CharacterManager character, CharacterManager target) {
            return true;
        }
    }
}