using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [CreateAssetMenu(menuName ="Skills/Elements", fileName = "Element")]
    public class SkillElement : ScriptableObject
    {
        public Texture2D elementImage;
        public string elementName;

        public List<SkillElement> weaknesses;
        public List<SkillElement> resistances;

        public bool IsWeak(SkillElement element) {
            return weaknesses.Contains(element);
        }

        public bool Resists(SkillElement element) {
            return resistances.Contains(element);
        }

        public static List<SkillElement> JoinWeaknesses(SkillElement element1, SkillElement element2) {
            List<SkillElement> result = new List<SkillElement>();

            JoinList(result, element1.weaknesses);
            JoinList(result, element2.weaknesses);
            ExcludeList(result, element1.resistances);
            ExcludeList(result, element2.resistances);
            
            return result;
        }

        public static List<SkillElement> JoinResistances(SkillElement element1, SkillElement element2) {
            List<SkillElement> result = new List<SkillElement>();

            JoinList(result, element1.resistances);
            JoinList(result, element2.resistances);
            ExcludeList(result, element1.weaknesses);
            ExcludeList(result, element2.weaknesses);

            return result;
        }

        public static void JoinList(List<SkillElement> combinedList, List<SkillElement> additions) {
            for (int i = 0; i < additions.Count; i++) {
                SkillElement element = additions[i];
                if (!combinedList.Contains(element)) {
                    combinedList.Add(element);
                }
            }
        }

        public static void ExcludeList(List<SkillElement> combinedList, List<SkillElement> exclusions) {
            for (int i = 0; i < exclusions.Count; i++) {
                SkillElement element = exclusions[i];
                if (combinedList.Contains(element)) {
                    combinedList.Remove(element);
                }
            }
        }
    }
}