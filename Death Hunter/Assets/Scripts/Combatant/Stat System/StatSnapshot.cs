using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem.StatSystem
{
    public class StatSnapshot
    {
        //public List<Stat> stats = new List<Stat>();
        private Dictionary<StatType, Stat> _statsDict = new Dictionary<StatType, Stat>();

        public StatSnapshot(List<Stat> newStats)
        {
            for(int i = 0; i < newStats.Count; i++)
            {
                Stat stat = newStats[i].Copy();
                //stats.Add(stat);
                _statsDict[stat.type] = stat;
            }
        }

        public int GetStat(StatType type)
        {
            return _statsDict[type].GetStatTotal();
        }
    }
}
