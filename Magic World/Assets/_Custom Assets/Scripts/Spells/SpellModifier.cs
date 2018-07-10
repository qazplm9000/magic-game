using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public class SpellModifier
    {

        public string _name;
        public string description;

        public float powerMultiplier = 1.0f;
        public float castTimeMultiplier = 1.0f;
        public float manaMultiplier = 1.0f;

        /// <summary>
        /// Applies some sort of effect upon casting
        /// </summary>
        public virtual void OnCast(){

        }

        /// <summary>
        /// Applies some sort of effect that happens on landing
        /// </summary>
        public virtual void OnLand() {

        }

    }
}