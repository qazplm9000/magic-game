using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComboSystem {
    [System.Serializable]
    public class CharacterCombos : MonoBehaviour {


        public List<ComboHit> combo = new List<ComboHit>();

        public int comboCount = 0;
        private float comboTimer = 0f;
        public float comboTimeLimit = 0.2f;

        private CombatController user;


        public void Start()
        {
            user = transform.GetComponent<CombatController>();
        }



        public void Update()
        {
            if (comboCount > 0 && !user.lockedMovement) {
                comboTimer += Time.deltaTime;
            }

            if (comboTimer >= comboTimeLimit) {
                ComboReset();
            }
        }


        public void Attack() {

            comboTimer = 0;

            if (comboCount < combo.Count) {
                StartCoroutine(combo[comboCount].Attack(user));
            }

            comboCount++;

            if (comboCount == combo.Count) {
                ComboReset();
            }
        }


        public void ComboReset() {
            comboCount = 0;
        }


    }
}