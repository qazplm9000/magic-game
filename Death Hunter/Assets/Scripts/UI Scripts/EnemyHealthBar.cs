using CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TargettingSystem;

public class EnemyHealthBar : HealthBar
{

    protected override ITargettable GetCharacter()
    {
        ITargettable target = character.GetCurrentTarget();

        return target;
    }
}

