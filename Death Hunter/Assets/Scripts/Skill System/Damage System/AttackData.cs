using CombatSystem;
using CombatSystem.StatSystem;


namespace SkillSystem
{
    public class AttackData
    {
        public Combatant caster;
        public StatSnapshot casterSnapshot;
        public Combatant target;
        public int potency;
        public Element element;
        public bool isHealing;

        public AttackData(Combatant caster, Combatant target, int potency, Element element, bool isHealing)
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