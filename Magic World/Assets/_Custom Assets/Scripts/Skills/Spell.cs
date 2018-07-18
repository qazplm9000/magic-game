using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [CreateAssetMenu(menuName ="Skills/Spell",fileName ="Spell")]
    public class Spell : Skill
    {

        
        public GameObject spellObject;
        public MonoBehaviour spellBehaviour;

        public Spell() {
            
        }

        public void CastSpell() {

        }
    }
}