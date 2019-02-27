using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Ability Properties/Element")]
    public class AbilityElement : ScriptableObject
    {
        public string elementName;
        public Sprite elementImage;
        public AbilityElement compatibleElement;
        public AbilityElement incompatibleElement;
    }
}