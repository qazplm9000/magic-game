using CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    
    public abstract class Weapon : ScriptableObject
    {


        public abstract void Attack(Combatant attacker);
    }
}
