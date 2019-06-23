using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PartySystem;
using AbilitySystem;

namespace BattleSystem
{
    [System.Serializable]
    public class BattleContext
    {
        public Party playerParty;
        public Party enemyParty;
        public Party miscParty;

        [Header("Field Element")]
        public bool lockFieldElement = false;
        public AbilityElement fieldElement;
        public AbilityElement nullElement;


        public TurnOrder turnOrder;

        public CharacterManager currentTurn;

        public List<CharacterManager> targets = new List<CharacterManager>();


        public BattleContext() {
            playerParty = new Party();
            enemyParty = new Party();
            miscParty = new Party();
            targets = new List<CharacterManager>();
        }

        




        /// <summary>
        /// Clears the list of targets
        /// </summary>
        public void EndWarning() {
            Debug.Log("Warning ended");
            targets = new List<CharacterManager>();
        }


        /// <summary>
        /// Call as soon as characters can start defending
        /// </summary>
        /// <param name="target"></param>
        /// <param name="delay"></param>
        public void WarnTarget(CharacterManager target, float delay) {
            if (!targets.Contains(target))
            {
                targets.Add(target);
                Debug.Log("Warned " + target.name);
            }
        }

        public bool PlayerPartyIsDead() {
            return false;
        }

        public bool EnemyPartyIsDead() {
            return false;
        }

        public bool IsTargetted(CharacterManager character) {
            return targets.Contains(character);
        }

    }
}