using CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EffectSystem
{
    public class EffectObject
    {
        public List<StatusEffect> statuses;

        private float previousFrame;
        private float currentFrame;
        private Combatant user;
        private Combatant target;
        private bool isFinished = false;

        public void StartEffect(Combatant user, Combatant target) {
            this.user = user;
            this.target = target;
        }

        public void RunEffect() {
            Tick();
            isFinished = true;
            for (int i = 0; i < statuses.Count; i++) {
                StatusEffect status = statuses[i];
                status.RunStatus(previousFrame, currentFrame, user, target);
                isFinished &= status.StatusIsFinished(previousFrame, currentFrame);
            }
        }

        public bool IsFinished() {
            return isFinished;
        }

        private void Tick() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;
        }

        private void ResetEffect() {
            user = null;
            target = null;
            previousFrame = 0;
            currentFrame = 0;
        }
    }
}
