using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public abstract class CombatantCondition
    {
        public abstract bool ConditionIsTrue(Combatant character);
    }
}
