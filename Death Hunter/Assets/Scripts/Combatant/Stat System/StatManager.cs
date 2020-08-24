using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace CombatSystem.StatSystem
{
    public class StatManager : MonoBehaviour, ISerializationCallbackReceiver
    {
        public StatPreset preset = null;
        public List<Stat> stats = new List<Stat>();
        public Dictionary<StatType, Stat> _statsDict = new Dictionary<StatType, Stat>();

        private void Awake()
        {
            StatsToDict();
        }

        /// <summary>
        /// Gets the total value of the stat
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public int GetStat(StatType stat) {
            return _statsDict[stat].GetStatTotal();
        }
        
        

        /// <summary>
        /// Adds to the base value of a stat
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="amount"></param>
        public void AddStat(StatType stat, int amount) {
            if(stat == StatType.MaxHealth)
            {
                _statsDict[stat].AddStat(amount);
                _statsDict[StatType.CurrentHealth].AddBaseStat(amount);
            }else if(stat != StatType.CurrentHealth)
            {
                _statsDict[stat].AddStat(amount);
            }
        }

        public void RemoveStat(StatType stat, int amount)
        {
            if (stat == StatType.MaxHealth)
            {
                _statsDict[stat].RemoveStat(amount);
                _statsDict[StatType.CurrentHealth].RemoveBaseStat(amount);
            }
            else if (stat != StatType.CurrentHealth)
            {
                _statsDict[stat].RemoveStat(amount);
            }
        }

        public void TakeDamage(int amount)
        {
            _statsDict[StatType.CurrentHealth].RemoveBaseStat(amount);
            ClampCurrentHealth();
        }

        public void HealHealth(int amount)
        {
            GetStatObject(StatType.CurrentHealth).AddBaseStat(amount);
            ClampCurrentHealth();
        }

        public bool IsDead()
        {
            return _statsDict[StatType.CurrentHealth].GetStatTotal() <= 0;
        }

        public StatSnapshot CreateSnapshot()
        {
            return new StatSnapshot(stats);
        }




        private Stat GetStatObject(StatType type)
        {
            return _statsDict[type];
        }

        private void ClampCurrentHealth()
        {
            Stat ch = GetStatObject(StatType.CurrentHealth);
            Stat mh = GetStatObject(StatType.MaxHealth);
            ch.baseValue = Mathf.Clamp(ch.baseValue, 0, mh.GetStatTotal());
        }


        private void StatsToDict()
        {
            for(int i = 0; i < stats.Count; i++)
            {
                StatType type = stats[i].type;
                Stat stat = stats[i];

                _statsDict[type] = stat;
            }
        }


        public void OnBeforeSerialize()
        {
            if(Application.isEditor && preset != null && preset.stats.Count != stats.Count)
            {
                stats = new List<Stat>();
                List<Stat> presetStats = preset.stats;

                for(int i = 0; i < presetStats.Count; i++)
                {
                    stats.Add(presetStats[i].Copy());
                }
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}