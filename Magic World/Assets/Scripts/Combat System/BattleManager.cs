using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class BattleManager : MonoBehaviour
    {
        public List<CharacterManager> characters;
        
        // Start is called before the first frame update
        void Start()
        {
            characters = new List<CharacterManager>(GameObject.FindObjectsOfType<CharacterManager>());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}