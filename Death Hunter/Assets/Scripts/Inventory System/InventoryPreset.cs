using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "Inventory Preset", menuName = "Item/_Inventory Preset")]
    public class InventoryPreset : ScriptableObject
    {
        public List<ItemSlot> slots = new List<ItemSlot>();

    }
}
