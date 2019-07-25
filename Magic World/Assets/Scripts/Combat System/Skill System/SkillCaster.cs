using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.SkillSystem
{
    public class SkillCaster : MonoBehaviour
    {
        private CharacterManager character;
        private Skill currentSkill = null;
        private CharacterManager target;
        private float currentFrame = 0f;
        private float previousFrame = 0f;

        // Start is called before the first frame update
        void Start()
        {
            character = transform.GetComponent<CharacterManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentSkill != null) {
                Tick();

                currentSkill.CastSkill(this, previousFrame, currentFrame);

                if (previousFrame == currentFrame) {

                }else if (currentSkill.IsSkillFinished(this, previousFrame, currentFrame)) {
                    currentSkill.OnCastFinished(this);
                    currentSkill = null;
                }
            }
        }

        private void Tick() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;
        }

        public void CastSkill(Skill skill) {
            if (currentSkill == null) {
                currentSkill = skill;
                currentSkill.OnCastStart(this);
            }
        }

        public bool IsCasting() {
            return currentSkill != null;
        }
    }
}