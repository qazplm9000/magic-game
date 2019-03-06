using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InventorySystem
{
    public class Inventory
    {
        private Dictionary<Item, int> _inventory;
        private Dictionary<ItemType, List<Item>> _inventoryCategories;
        private List<Item> allItems;

        public Inventory() {
            _inventory = new Dictionary<Item, int>();

            ItemType[] types = (ItemType[])Enum.GetValues(typeof(ItemType));
            for (int i = 0; i < types.Length; i++) {
                _inventoryCategories[types[i]] = new List<Item>();
            }

            allItems = new List<Item>();

        }

        public Inventory(List<Item> items) : base() {
            for (int i = 0; i < items.Count; i++) {
                
            }
        }


        /// <summary>
        /// Adds the item to the inventory
        /// If the item is also new, add it to the proper category
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        public void AddItem(Item item, int quantity = 1) {
            if (quantity < 1) {
                return;
            }

            if (_inventory.ContainsKey(item))
            {
                _inventory[item] += quantity;
            }
            else {
                //OnNewItemAdded();
                _inventory[item] = quantity;
                _inventoryCategories[item.GetItemType()].Add(item);
                allItems.Add(item);
            }
        }



        /// <summary>
        /// Removes the item from the inventory
        /// If there are no more copies of the item, then remove it from the category list as well
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        public void RemoveItem(Item item, int quantity = 1) {
            if (quantity < 1) {
                return;
            }

            if (_inventory.ContainsKey(item)) {
                _inventory[item] -= quantity;

                if (_inventory[item] <= 0) {
                    _inventory.Remove(item);
                    _inventoryCategories[item.GetItemType()].Remove(item);
                    allItems.Remove(item);
                }
            }
        }



        /// <summary>
        /// Checks if there are enough copies of an item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool HasEnoughOfItem(Item item, int quantity = 1) {
            bool result = false;

            if (_inventory.ContainsKey(item)) {
                if (_inventory[item] >= quantity) {
                    result = true;
                }
            }

            return result;
        }


    }
}