using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using TargettingSystem;
using UnityEngine;

namespace SkillSystem
{
    public class Caster : MonoBehaviour
    {


        private Animator anim;

        public Skill testSkill;
        private Skill currSkill;
        private SkillCastData castData;
        private bool isCasting = false;

        // Start is called before the first frame update
        void Start()
        {
            anim = transform.GetComponent<Animator>();
        }


        // Update is called once per frame
        void Update()
        {
            if(isCasting && !currSkill.IsRunning(castData))
            {
                ResetCast();
            }
        }



        public void CastSkill(Skill skill, ITargettable target)
        {
            if (!isCasting && skill != null)
            {
                castData = new SkillCastData(this, target);
                currSkill = skill;
                isCasting = true;
                StartCoroutine(skill.RunSkill(castData));
            }
        }

        private void ResetCast()
        {
            currSkill = null;
            castData = null;
            isCasting = false;
        }


        public void TakeDamage(int damage)
        {
            Debug.Log($"{name} took {damage} damage");
        }

        public void HealHealth(int healing)
        {
            Debug.Log($"{name} healed {healing} health");
        }





        public void PlayAnimation(string animationName, float crossfade)
        {
            anim.CrossFade(animationName, crossfade, 0, 0);
        }

        public bool IsEnemy(Caster target)
        {
            return target.tag != tag;
        }

    }
}