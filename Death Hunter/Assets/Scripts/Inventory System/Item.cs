using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{

    public enum ItemType
    {
        Consumable,
        Helmet,
        Chest,
        Legs,
        Accessory,
        Weapon
    }

    public abstract class Item : ScriptableObject
    {
        public int id;
        public ItemType type;
        public new string name;
        public Sprite icon;

        public virtual void Use(Combatant user)
        {
            Debug.Log("Used item");
        }

        public new ItemType GetType()
        {
            return type;
        }

    }
}