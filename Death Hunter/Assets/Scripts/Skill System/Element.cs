using UnityEngine;
using System.Collections;

namespace SkillSystem
{
    [CreateAssetMenu(menuName = "Skill/Element")]
    public class Element : ScriptableObject
    {
        public string elementName;
        public Sprite elementSprite;
    }
}