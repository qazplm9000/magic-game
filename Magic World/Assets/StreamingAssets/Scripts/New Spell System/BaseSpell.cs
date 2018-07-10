using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpellSystem
{
    public abstract class BaseSpell : ScriptableObject
    {

        public string spellName;
        public int spellPower;
        public GameObject spellObject;
        public GameObject spellCastEffect;
        public GameObject spellLandEffect;
        public Sprite spellIcon;
        public SpellElement spellElement;

        public abstract void SpellAction(GameObject spellObj);
    }
}