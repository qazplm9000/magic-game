using CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EffectSystem
{

    [Serializable]
    public class EffectData
    {
        public Combatant caster;
        public Combatant target;
        public int potency;
        public Element element;
        private Timer timer;


        public EffectData(Combatant caster, Combatant target, int potency, Element element)
        {
            timer = new Timer();
            this.caster = caster;
            this.target = target;
            this.potency = potency;
            this.element = element;
        }

        public void Tick()
        {
            timer.Tick();
        }

        public bool PastTime(float time)
        {
            return timer.PastTime(time);
        }

        public bool AtTime(float time)
        {
            return timer.AtTime(time);
        }

        public bool AtInterval(float period)
        {
            return timer.AtInterval(period);
        }

        public int TimesAtInterval(float period)
        {
            return timer.TotalIntervals(period);
        }
    }

    public abstract class Effect : ScriptableObject
    {
        public string description;
        public float duration;

        public abstract void RunEffect(EffectData data);
        public abstract bool EffectIsFinished(EffectData data);

        public abstract void UpdateDescription();
    }
}
