using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.EquipmentSystem
{
    public class Equipment : Item
    {

        //Stats to increase

        public Equipment() {
            
        }

        public ItemType GetItemType()
        {
            return ItemType.Equipment;
        }

        public bool IsConsumable()
        {
            return false;
        }
    }
}