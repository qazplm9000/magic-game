using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    [CreateAssetMenu(menuName = "Character State/State Tree")]
    public class CharacterStateTree : ScriptableObject
    {
        public List<CharacterState> states;

        

    }
}