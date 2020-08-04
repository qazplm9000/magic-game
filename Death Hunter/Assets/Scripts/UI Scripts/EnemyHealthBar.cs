using CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EnemyHealthBar : HealthBar
{

    protected override Combatant GetCharacter()
    {
        Combatant target = character.GetTarget();

        return target;
    }
}

