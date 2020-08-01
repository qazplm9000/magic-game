using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{

    public enum ItemType
    {
        Consumable,
        Equipment
    }

    public abstract class Item : ScriptableObject
    {
        public int id;
        public ItemType type;
        public new string name;
        public Sprite icon;

        public virtual void Use()
        {
            Debug.Log("Used item");
        }


    }
}