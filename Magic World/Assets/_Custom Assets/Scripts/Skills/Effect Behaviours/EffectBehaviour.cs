using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class EffectBehaviour : MonoBehaviour
    {

        Transform effectLocation;
        //need to location to instantiate effect

        public void Update()
        {
            
        }

        public virtual void InitializeEffect(SkillEffectData data)
        {

        }

        private void OnDisable()
        {
            this.enabled = false;
        }
    }
}