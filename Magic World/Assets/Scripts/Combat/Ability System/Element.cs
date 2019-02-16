using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Elements/Element")]
    public class Element : ScriptableObject
    {
        public string elementName;
        //public Texture elementImage;
        public Element compatibleElement;
        public Element incompatibleElement;
    }
}