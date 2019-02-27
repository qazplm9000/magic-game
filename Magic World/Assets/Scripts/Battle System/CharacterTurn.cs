using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    [System.Serializable]
    public class CharacterTurn
    {
        public CharacterManager character;
        public float turnTime;

        public CharacterTurn(CharacterManager newCharacter, float newTurnTime) {
            character = newCharacter;
            turnTime = newTurnTime;
        }
    }
}