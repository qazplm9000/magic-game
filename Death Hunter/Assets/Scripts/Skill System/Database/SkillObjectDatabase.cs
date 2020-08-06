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
        public string description;
        public string objName;
        public int id;
        public SkillObject skillObj;

        public SkillObjectDatabaseNode(int newId, SkillObject obj)
        {
            objName = obj.name;
            skillObj = obj;
            id = newId;
        }
    }

    [CreateAssetMenu(fileName = "Skill Object Database", menuName = "Databases/Skill Object")]
    public class SkillObjectDatabase : ScriptableObject, ISerializationCallbackReceiver
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

        /// <summary>
        /// Used by SkillAnimation to update description
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string SlowGetObjectNameByID(int id)
        {
            string result = "Invalid Object";
            for(int i = 0; i < skillObjects.Count; i++)
            {
                if(skillObjects[i].id == id)
                {
                    result = skillObjects[i].objName;
                }
            }
            return result;
        }




        public void OnBeforeSerialize()
        {
            if (Application.isEditor)
            {
                for (int i = 0; i < skillObjects.Count; i++)
                {
                    SkillObjectDatabaseNode node = skillObjects[i];
                    node.objName = node.skillObj == null ? "<Blank>" : node.skillObj.name;
                    node.description = $"{node.id} - {node.objName}";
                }
            }
        }

        public void OnAfterDeserialize()
        {
        }
    }
}
