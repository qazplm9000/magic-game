using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EffectSystem
{
    [System.Serializable]
    public class Effect
    {
        public EffectType type;
        public EffectTiming timing;
        public float duration;
        public float period;


        public void RunEffect(float previousFrame, float currentFrame, Combatant user, Combatant target) {
            if (EffectShouldBeApplied(previousFrame, currentFrame))
            {
                if (timing == EffectTiming.Periodically)
                {
                    for (int j = 0; j < TimesToRun(previousFrame, currentFrame); j++)
                    {
                        ApplyEffect(user, target);
                    }
                }
                else
                {
                    ApplyEffect(user, target);
                }
            }
        }

        public void ApplyEffect(Combatant user, Combatant target) {
            switch (type)
            {
                case EffectType.DealDamage:
                    target.TakeDamage(5);
                    break;
                case EffectType.HealHealth:
                    target.TakeDamage(-5);
                    break;
                case EffectType.SetFlag:
                    
                    break;
            }
        }

        public bool EffectShouldBeApplied(float previousFrame, float currentFrame) {
            bool result = false;

            switch (timing)
            {
                case EffectTiming.OnStart:
                    result = previousFrame == 0;
                    break;
                case EffectTiming.OnStop:
                    result = currentFrame > duration;
                    break;
                case EffectTiming.EveryFrame:
                    result = true;
                    break;
                case EffectTiming.AfterDelay:
                    result = previousFrame < period && currentFrame > period;
                    break;
                case EffectTiming.Periodically:
                    if (period == 0) {
                        result = true;
                        break;
                    }
                    int prevDiv = (int)(previousFrame / period);
                    int currDiv = (int)(currentFrame / period);
                    result = prevDiv != currDiv;
                    break;
            }

            return result;
        }

        public int TimesToRun(float previousFrame, float currentFrame) {
            int result = 0;

            switch (timing)
            {
                case EffectTiming.OnStart:
                    result = EffectShouldBeApplied(previousFrame, currentFrame) ? 1 : 0;
                    break;
                case EffectTiming.OnStop:
                    result = EffectShouldBeApplied(previousFrame, currentFrame) ? 1 : 0;
                    break;
                case EffectTiming.EveryFrame:
                    result = EffectShouldBeApplied(previousFrame, currentFrame) ? 1 : 0;
                    break;
                case EffectTiming.AfterDelay:
                    result = EffectShouldBeApplied(previousFrame, currentFrame) ? 1 : 0;
                    break;
                case EffectTiming.Periodically:
                    if (period == 0) {
                        result = 1;
                        break;
                    }
                    int prevDiv = (int)(previousFrame / period);
                    int currDiv = (int)(currentFrame / period);
                    result = currDiv - prevDiv;
                    break;
            }

            return result;
        }

        public bool EffectIsFinished(float previousFrame, float currentFrame) {
            return currentFrame > duration;
        }


    }
}