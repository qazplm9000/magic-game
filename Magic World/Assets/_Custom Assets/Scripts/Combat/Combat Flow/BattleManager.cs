using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Handles the combat logic
namespace CombatSystem
{
    public class BattleManager : MonoBehaviour
    {

        //list of all characters
        //public List<Combatant> combatants
        //public List<Combatant> players
        //public List<Combatant> enemies
        public static BattleManager battleManager;

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
            if (battleManager == null)
            {
                battleManager = this;
            }
            else {
                Destroy(this);
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (actionTaken) {
                turnTimer -= Time.deltaTime;

                if (turnTimer < turnTime) {
                    ProgressTurn();
                }
            }


        }



        public void ProgressTurn() {
            actionTaken = false;
            //switch to next character
            //currentCharacter = nextCombatant
            //call turn event
        }


    }
}