using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Spell")]
public class BaseSpell : ScriptableObject{

    public string spellName;
    public int spellpower;
    public SpellElement element;
    public float castTime;
    public int mpCost;
    public GameObject spellEffect;
    public GameObject spellObject;
    public SpellBehavior behavior;
}

