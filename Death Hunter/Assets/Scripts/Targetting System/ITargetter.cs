using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargettingSystem
{
    public interface ITargetter
    {
        ITargettable TargetEnemy(bool targetNext = true);
        ITargettable TargetAlly(bool targetNext = true);
        ITargettable GetCurrentTarget();
        void Untarget();
    }
}