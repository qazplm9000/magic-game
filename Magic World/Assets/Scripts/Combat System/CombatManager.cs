using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class CombatManager : MonoBehaviour
    {
        public List<Combatant> characters;

        public float timer = 5f;
        public float timeRemaining = 5f;

        public Combatant currentTurn = null;

        public TurnOrder turnOrder;

        public ObjectPool pool;

        public Camera camera;




        // Start is called before the first frame update
        void Start()
        {
            characters = new List<Combatant>(GameObject.FindObjectsOfType<Combatant>());
            turnOrder = new TurnOrder(characters);
            camera = Camera.main;
            pool = transform.GetComponent<ObjectPool>();
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
                Combatant character = characters[i];
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


        public void RemoveObject(GameObject obj) {
            obj.SetActive(false);
        }

        public GameObject PullObject(GameObject obj) {
            return pool.PullObject(obj);
        }

    }
}