using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Element")]
public class Element : ScriptableObject
{
    public Sprite icon;
    public string elementName;
}

