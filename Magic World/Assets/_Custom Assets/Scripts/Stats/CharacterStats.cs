using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;


public class CharacterStats : MonoBehaviour{


    public HealthStat health;
    public ManaStat mana;

    public Stat magic;
    public Stat endurance;
    public Stat speed;

    public List<MagicLevel> magicStats;
    private Dictionary<Element, MagicLevel> magicDict; // used for speeding up searches

    private void Start()
    {
        magicDict = new Dictionary<Element, MagicLevel>();
    }


    /// <summary>
    /// Character takes damage
    /// </summary>
    /// <param name="spell"></param>
    /// <param name="enemyStats"></param>
    public void TakeDamage(Spell spell, CharacterStats enemyStats) {
        float enemyDamage = enemyStats.magic.totalValue + (spell.spellpower * Mathf.Sqrt(ElementLevel(spell.element + 5)));
        float playerDefense = endurance.totalValue * Mathf.Sqrt(enemyStats.ElementLevel(spell.element) + 5);
        int totalDamage = (int)(enemyDamage + playerDefense);

        health.TakeDamage(totalDamage);
    }


    /// <summary>
    /// Adds magic to the list of levels if it does not already exist
    /// Returns true if it gets added, false if not
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public bool AddMagicLevel(MagicLevel level) {
        bool result = false;

        if (!magicDict.ContainsKey(level.element))
        {
            magicStats.Add(level);
            magicDict[level.element] = level;
            result = true;
        }

        return result;
    }


    /// <summary>
    /// Removes magic of the element from the list
    /// </summary>
    public bool RemoveMagicLevel(Element element) {
        bool result = false;

        if (magicDict.ContainsKey(element)) {
            int index = -1;

            for (int i = 0; i < magicStats.Count; i++) {
                if (magicStats[i].element == element) {
                    index = i;
                    break;
                }
            }

            magicStats.RemoveAt(index);

            magicDict.Remove(element);
            result = true;
        }

        return result;
    }



    public int ElementLevel(Element element) {
        int result = 0;

        if (magicDict.ContainsKey(element)) {
            result = magicDict[element].level;
        }

        return result;
    }

    private void RemakeDict() {
        magicDict = new Dictionary<Element, MagicLevel>();
        foreach (MagicLevel level in magicStats) {
            magicDict[level.element] = level;
        }
    }
}
