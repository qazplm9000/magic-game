using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    [System.Serializable]
    public class CharacterTurn
    {
        public CharacterManager character;
        public int turnTime;

        public CharacterTurn(CharacterManager newCharacter, int newTurnTime) {
            character = newCharacter;
            turnTime = newTurnTime;
        }
    }
}