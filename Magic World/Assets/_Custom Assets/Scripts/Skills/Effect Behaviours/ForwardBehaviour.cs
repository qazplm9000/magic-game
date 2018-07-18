using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public class ForwardBehaviour : EffectBehaviour
    {
        

        // Update is called once per frame
        void Update()
        {
            transform.position += transform.forward * 5 * Time.deltaTime;
        }
    }
}