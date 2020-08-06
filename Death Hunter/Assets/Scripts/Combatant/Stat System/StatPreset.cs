using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem.StatSystem
{
    [CreateAssetMenu(fileName = "Stat Preset", menuName = "Stats/Preset")]
    public class StatPreset : ScriptableObject
    {
        public List<Stat> stats = new List<Stat>();
    }
}
