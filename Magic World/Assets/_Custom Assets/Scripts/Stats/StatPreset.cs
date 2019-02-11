using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(menuName = "Stats/Stat Preset")]
    public class StatPreset : ScriptableObject
    {
        public List<Stat> stats;
    }
}