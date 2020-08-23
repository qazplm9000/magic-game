using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargettingSystem
{
    public interface ITargetter
    {
        Combatant TargetEnemy(bool targetNext = true);
        Combatant TargetAlly(bool targetNext = true);
        Combatant GetCurrentTarget();
        void Untarget();
    }
}