using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.SkillSystem
{
    public class SkillObjectController : MonoBehaviour
    {

        public SkillObjectBehaviour behaviour;
        public Skill skill;
        public Combatant caster;
        public Combatant target;
        public SkillObjectData data;

        public float previousFrame = 0;
        public float currentFrame = 0;

        // Start is called before the first frame update
        void Start()
        {
            data = new SkillObjectData();
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.gameObject.activeInHierarchy) {
                Tick();
                behaviour.Run(this);
            }

        }




        public void Tick(){
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;
        }


        public void InitObject(SkillObjectBehaviour newBehaviour, Skill newSkill, Combatant newCaster, Combatant newTarget)
        {
            behaviour = newBehaviour;
            skill = newSkill;
            caster = newCaster;
            target = newTarget;
        }

        public void Kill() {
            previousFrame = 0;
            currentFrame = 0;

            transform.gameObject.SetActive(false);
            behaviour = null;
            caster = null;
            target = null;
            skill = null;

            data.ResetData();
        }
    }
}