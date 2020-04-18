using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    public class EffectManager : MonoBehaviour
    {
        private Combatant user;

        public List<Effect> currentEffects;

        // Start is called before the first frame update
        void Start()
        {
            currentEffects = new List<Effect>();
            user = transform.GetComponent<Combatant>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}