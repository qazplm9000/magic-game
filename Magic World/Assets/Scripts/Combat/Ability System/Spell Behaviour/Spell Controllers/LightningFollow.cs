using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class LightningFollow : SpellController
    {
        public override void Execute(SpellBehaviour spell)
        {
            Vector3 targetPosition = spell.target.transform.position;
            Vector3 spellPosition = spell.transform.position;
        }
    }
}