using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatSystem;
using UnityEngine;

namespace TargettingSystem
{
    public class EnemyTargetter : MonoBehaviour, ITargetter
    {
        public Combatant currentTarget;


        public Combatant GetCurrentTarget()
        {
            return currentTarget;
        }

        public Combatant TargetAlly(bool targetNext = true)
        {
            return null;
        }

        public Combatant TargetEnemy(bool targetNext = true)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            currentTarget = playerObj.GetComponent<Combatant>();
            return currentTarget;
        }

        public void Untarget()
        {
            currentTarget = null;
        }
    }
}
