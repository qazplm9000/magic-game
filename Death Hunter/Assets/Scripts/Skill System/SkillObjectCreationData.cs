using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    [System.Serializable]
    public class SkillObjectCreationData
    {
        public float lifetime;
        public int objIndex;
        public int _objIndex = -1;
        public SkillObjectLocation location;
        public SkillObjectParent parent;
        public Vector3 positionOffset;
        public Vector3 rotationOffset;
        public float movementSpeed = 5;
        public float rotationSpeed = 100;
        public float scale = 1;
    }

    [Serializable]
    public class SkillObjectCreation : ISerializationCallbackReceiver
    {
        public string description = "";
        public float startTime = 0;
        public SkillObjectCreationData creationData;
        public List<SkillEffect> objEffects = new List<SkillEffect>();

        public void CreateObject(SkillCastData skillData)
        {
            if (skillData.AtTime(startTime))
            {
                SkillObject createdObj = CreateSkillObject(creationData.objIndex);

                SkillObjectData soData = new SkillObjectData(skillData, objEffects, creationData.lifetime);
                createdObj.StartSkill(soData);

                Transform locationTransform = GetLocationTransform(skillData, creationData.location);
                SetObjectTransform(locationTransform, createdObj, creationData.positionOffset);
                SetObjectRotation(locationTransform, createdObj, creationData.rotationOffset);
                SetObjectScale(locationTransform, createdObj, creationData.scale);
            }
        }

        private void SetObjectTransform(Transform location, SkillObject createdObj, Vector3 positionOffset)
        {
            createdObj.transform.position = location.position +
                                            location.forward * positionOffset.z +
                                            location.right * positionOffset.x +
                                            location.up * positionOffset.y;
        }

        private void SetObjectRotation(Transform location, SkillObject createdObj, Vector3 rotationOffset)
        {
            createdObj.transform.rotation = location.transform.rotation *
                                            Quaternion.Euler(rotationOffset.x, 0, 0) *
                                            Quaternion.Euler(0, rotationOffset.y, 0) *
                                            Quaternion.Euler(0, 0, rotationOffset.z);
        }

        private void SetObjectScale(Transform location, SkillObject createdObj, float scale)
        {
            createdObj.transform.localScale = new Vector3(scale, scale, scale);
        }

        private Transform GetLocationTransform(SkillCastData data, SkillObjectLocation location)
        {
            Transform result = null;

            switch (location)
            {
                case SkillObjectLocation.Creator:
                    result = data.caster.transform;
                    break;
                case SkillObjectLocation.Caster:
                    result = data.caster.transform;
                    break;
                case SkillObjectLocation.Target:
                    result = data.target.transform;
                    break;
            }

            return result;
        }

        private SkillObject CreateSkillObject(int searchedIndex)
        {
            Debug.Log("Pulled skill object");
            return WorldManager.PullSkillObject(searchedIndex);
        }



        /*
            Description Updater
             */
        public void UpdateDescription()
        {
            if (creationData.objIndex != creationData._objIndex)
            {
                WorldManager world = GameObject.FindObjectOfType<WorldManager>();
                string objName = world.skillObjects.SlowGetObjectNameByID(creationData.objIndex);
                description = $"{startTime} - {objName}";
                creationData._objIndex = creationData.objIndex;
            }
        }

        public void OnBeforeSerialize()
        {
            if (Application.isEditor)
            {
                UpdateDescription();
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}
