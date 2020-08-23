using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectSystem
{
    public class DamageEffect : Effect
    {
        public EffectTiming timing;
        public float potencyModifier;

        public override void RunEffect(EffectData data)
        {
            data.target.TakeDamage((int)(data.potency * potencyModifier));
        }

        public override bool EffectIsFinished(EffectData data)
        {
            return true;
        }


        public override void UpdateDescription()
        {
            
        }
    }
}
