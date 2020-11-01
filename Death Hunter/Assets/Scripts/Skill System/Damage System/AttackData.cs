using CombatSystem;
using CombatSystem.StatSystem;


namespace SkillSystem
{
    public class AttackData
    {
        public IDamageable caster;
        public StatSnapshot casterSnapshot;
        public IDamageable target;
        public int potency;
        public Element element;
        public bool isHealing;

        public AttackData(IDamageable caster, IDamageable target, int potency, Element element, bool isHealing)
        {
            this.caster = caster;
            casterSnapshot = caster.CreateStatSnapshot();
            this.target = target;
            this.potency = potency;
            this.element = element;
            this.isHealing = isHealing;
        }

        public int GetCasterStat(StatType stat)
        {
            return casterSnapshot.GetStat(stat);
        }

        public int GetTargetStat(StatType stat)
        {
            return target.GetStat(stat);
        }
    }

}