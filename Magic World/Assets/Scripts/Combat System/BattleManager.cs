using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class BattleManager : MonoBehaviour
    {
        public List<CharacterManager> characters;

        public float timer = 5f;
        public float timeRemaining = 5f;

        public CharacterManager currentTurn = null;

        public TurnOrder turnOrder;


        public Camera camera;




        // Start is called before the first frame update
        void Start()
        {
            characters = new List<CharacterManager>(GameObject.FindObjectsOfType<CharacterManager>());
            turnOrder = new TurnOrder(characters);
            camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {

            if (currentTurn != null)
            {
                if (timeRemaining > 0)
                {
                    AllCharactersAct();
                    timeRemaining -= Time.deltaTime;
                }
                else {
                    ProgressTurn();
                }
            }
            else {
                if (characters.Count > 0)
                {
                    currentTurn = characters[0];
                }
                else {
                    Debug.Log("Error: No characters present");
                }
            }


        }

        private void AllCharactersAct() {
            for (int i = 0; i < characters.Count; i++) {
                CharacterManager character = characters[i];
                if (character == currentTurn)
                {
                    character.TakeTurn(this);
                }
                else {
                    character.Idle(this);
                }
            }
        }


        public void ProgressTurn() {
            currentTurn.ProgressTurn();
            turnOrder.UpdateTurnOrder();
            currentTurn = turnOrder.GetCurrentTurn();

            timeRemaining = timer;
        }




    }
}