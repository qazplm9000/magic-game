using CombatSystem;
using CombatSystem.StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ItemSystem
{
    [CreateAssetMenu(fileName = "Consumable Item", menuName = "Item/Consumable")]
    public class ConsumableItem : Item
    {
        public int amount;

        public override void Use(Combatant target)
        {
            base.Use(target);
            target.HealHealth(amount);
        }
    }
}