using CombatSystem;
using SkillSystem;
using StateSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EffectSystem
{
    public enum EffectTiming
    {
        OnStart,
        OnStop,
        EveryFrame,
        AfterDelay,
        Periodically
    }

    public enum EffectType
    {
        DealDamage,
        HealHealth,
        SetFlag
    }

    
    [CreateAssetMenu(menuName = "Skill/Effects/Basic Effect")]
    public class BasicEffect : Effect, ISerializationCallbackReceiver
    {
        public EffectType type;
        public EffectTiming timing;
        public bool applyToCaster;
        public float period;

        public Flag flag;
        public bool flagValue;

        public override void RunEffect(EffectData data) {
            if (EffectShouldBeApplied(data))
            {
                if (timing == EffectTiming.Periodically)
                {
                    for (int j = 0; j < TimesToRun(data); j++)
                    {
                        ApplyEffect(data);
                    }
                }
                else
                {
                    ApplyEffect(data);
                }
            }
        }


        public override bool EffectIsFinished(EffectData data)
        {
            return data.AtTime(duration);
        }


        public override void UpdateDescription()
        {
            switch (type)
            {
                case EffectType.DealDamage:
                    description = $"{type.ToString()} - {timing.ToString()}";
                    break;
                case EffectType.HealHealth:
                    description = $"{type.ToString()} - {timing.ToString()}";
                    break;
                case EffectType.SetFlag:
                    description = $"{type.ToString()} - {timing.ToString()} - {flagValue}";
                    break;
            }
        }



        /*********************
            private functions
            *********************/


        private void ApplyEffect(EffectData data) {
            IDamageable target = null;

            if (applyToCaster)
            {
                target = data.caster;
            }
            else
            {
                target = data.target;
            }

            switch (type)
            {
                case EffectType.DealDamage:
                    target.TakeDamage(data.potency);
                    break;
                case EffectType.HealHealth:
                    target.TakeDamage(-data.potency);
                    break;
                case EffectType.SetFlag:
                    target.ChangeFlag(flag, flagValue);
                    break;
            }
        }

        private bool EffectShouldBeApplied(EffectData data) {
            bool result = false;

            switch (timing)
            {
                case EffectTiming.OnStart:
                    result = data.AtTime(0);
                    break;
                case EffectTiming.OnStop:
                    result = data.AtTime(duration);
                    break;
                case EffectTiming.EveryFrame:
                    result = true;
                    break;
                case EffectTiming.AfterDelay:
                    result = data.AtTime(period);
                    break;
                case EffectTiming.Periodically:
                    if (period == 0) {
                        result = true;
                        break;
                    }
                    Debug.Log("Periodical Effect not implemented");
                    break;
            }

            return result;
        }

        private int TimesToRun(EffectData data) {
            int result = 0;

            switch (timing)
            {
                case EffectTiming.OnStart:
                    result = EffectShouldBeApplied(data) ? 1 : 0;
                    break;
                case EffectTiming.OnStop:
                    result = EffectShouldBeApplied(data) ? 1 : 0;
                    break;
                case EffectTiming.EveryFrame:
                    result = EffectShouldBeApplied(data) ? 1 : 0;
                    break;
                case EffectTiming.AfterDelay:
                    result = EffectShouldBeApplied(data) ? 1 : 0;
                    break;
                case EffectTiming.Periodically:
                    if (period == 0) {
                        result = 1;
                    }
                    else
                    {
                        result = data.TimesAtInterval(period);
                    }
                    break;
            }

            return result;
        }

        public void OnBeforeSerialize()
        {
            UpdateDescription();
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}