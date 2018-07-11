using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpellSystem
{
    [System.Serializable]
    public class Spell
    {


        public int spellpower = 10;
        public float castTime = 1f;
        public int manaCost = 10;
        public GameObject spellObject;

        public CharacterStats casterStats;

        public Spell() {
            
        }

        public void CastSpell() {

        }
    }
}