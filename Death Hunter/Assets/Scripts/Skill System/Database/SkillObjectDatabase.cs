using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    [System.Serializable]
    public class SkillObjectDatabaseNode
    {
        public string objName;
        public int id;
        public string description;
        public SkillObject skillObj;

        public SkillObjectDatabaseNode(int newId, SkillObject obj)
        {
            objName = obj.name;
            skillObj = obj;
            id = newId;
        }
    }

    [CreateAssetMenu(fileName = "Skill Object Database", menuName = "Databases/Skill Object")]
    public class SkillObjectDatabase : ScriptableObject
    {
        public List<SkillObjectDatabaseNode> skillObjects = new List<SkillObjectDatabaseNode>();
        private Dictionary<int, SkillObjectDatabaseNode> skillDict = new Dictionary<int, SkillObjectDatabaseNode>();
        
        public SkillObject GetObjectByID(int id)
        {
            return skillDict[id].skillObj;
        }

        public void AddSkillObject(SkillObject so)
        {
            SkillObjectDatabaseNode node = new SkillObjectDatabaseNode(skillObjects.Count + 1, so);
            skillObjects.Add(node);
        }

        public void AddSkillObject(int id, SkillObject so)
        {
            SkillObjectDatabaseNode node = new SkillObjectDatabaseNode(id, so);
            skillObjects.Add(node);
        }

        public void SetupDictionary()
        {
            skillDict = new Dictionary<int, SkillObjectDatabaseNode>();
            for(int i = 0; i < skillObjects.Count; i++)
            {
                skillDict[skillObjects[i].id] = skillObjects[i];
            }
        }
    }
}
