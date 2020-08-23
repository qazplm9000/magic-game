using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem
{
    public class IsEnemyCondition : CombatantCondition, ISerializationCallbackReceiver
    {
        public string description = "";
        public bool isEnemy = true;

        public override bool ConditionIsTrue(Combatant character)
        {
            return true;
        }

        public void OnAfterDeserialize()
        {
            if (isEnemy)
            {
                description = "Checks if target is an enemy";
            }
            else
            {
                description = "Checks if target is an ally";
            }
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}
