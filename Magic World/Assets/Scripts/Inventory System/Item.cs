using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public interface Item
    {
        bool IsConsumable();
        ItemType GetItemType();
    }
}