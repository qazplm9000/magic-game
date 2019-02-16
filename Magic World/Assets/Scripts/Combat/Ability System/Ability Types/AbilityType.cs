using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Ability Properties/Ability Type")]
    public class AbilityType : ScriptableObject
    {

        public string typeName;
        public Texture typeImage;
        

    }
}