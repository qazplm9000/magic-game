using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData {

    public Stat health;
    public Stat mana;
    public Stat magic;
    public Stat magicDefense;
    public List<MagicLevel> magicLevels;
}
