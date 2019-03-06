using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PartySystem
{
    public class Party
    {
        [Tooltip("Members in the party")]
        public List<CharacterManager> members;
        public int currentMana;
        public int maxMana;
        [Tooltip("Party's current element")]
        public FieldElement element;
        

        //public Inventory inventory
        

        /// <summary>
        /// Adds mana to the party
        /// </summary>
        /// <param name="mana"></param>
        public void RestoreMana(int mana) {
            currentMana += mana;
            currentMana = Mathf.Min(maxMana, currentMana);
        }

        /// <summary>
        /// Removes mana from the party
        /// </summary>
        /// <param name="mana"></param>
        public void UseMana(int mana) {
            currentMana -= mana;
            currentMana = Mathf.Max(0, currentMana);
        }


        /// <summary>
        /// Checks if party has enough mana
        /// </summary>
        /// <param name="mana"></param>
        /// <returns></returns>
        public bool HasEnoughMana(int mana) {
            return currentMana > mana;
        }



        public void AddMember(CharacterManager character) {
            if (!members.Contains(character)) {
                members.Add(character);
            }
        }

        /// <summary>
        /// Removes the character from the party
        /// Returns false if character was not found
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool RemoveMember(CharacterManager character) {
            bool result = false;

            int index = 0;

            while (index < members.Count) {
                if (members[index] == character) {
                    members.RemoveAt(index);
                    result = true;
                    break;
                }
                index++;
            }

            return result;
        }
    }
}