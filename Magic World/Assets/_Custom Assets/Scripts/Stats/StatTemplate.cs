using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class is used for creating customizeable 
/// global templates for stats
/// </summary>
[CreateAssetMenu(menuName = "Stat/Stat Template")]
public class StatTemplate : ScriptableObject {

    public List<StatType> stats = new List<StatType>();

}
