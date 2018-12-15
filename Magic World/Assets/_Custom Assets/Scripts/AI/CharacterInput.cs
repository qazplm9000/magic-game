using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

namespace InputSystem
{
    public abstract class CharacterBattleInput : ScriptableObject
    {

        //Call every frame during battle while a character is allowed to send inputs
        public abstract void Execute(BattleState battle, CharacterManager character);

    }
}