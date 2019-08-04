using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CombatSystem.CastLocationSystem
{
    public class CastLocationManager : MonoBehaviour, ISerializationCallbackReceiver
    {

        public List<CastLocation> positionTypes;
        public List<GameObject> castLocations;

        
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public GameObject GetBodyPart(CastLocation positionName) {
            GameObject result = castLocations[(int)positionName];

            if (result == null) {
                result = transform.gameObject;
            }

            return result;
        }





        public void OnAfterDeserialize()
        {

        }

        /// <summary>
        /// Maintains the size of the arrays
        /// </summary>
        public void OnBeforeSerialize()
        {
            CastLocation[] parts = (CastLocation[])Enum.GetValues(typeof(CastLocation));
            if (positionTypes == null || parts.Length != positionTypes.Count)
            {
                positionTypes = new List<CastLocation>(parts.Length);

                for (int i = 0; i < parts.Length; i++)
                {
                    positionTypes.Add(parts[i]);
                }
            }

            if (castLocations == null)
            {
                castLocations = new List<GameObject>(parts.Length);

                for (int i = 0; i < parts.Length; i++)
                {
                    castLocations.Add(null);
                }
            }
            else if (castLocations.Count < positionTypes.Count)
            {
                int count = castLocations.Count;
                for (int i = count; i < positionTypes.Count; i++)
                {
                    castLocations.Add(null);
                }
            }
            else if (castLocations.Count > positionTypes.Count)
            {
                int count = castLocations.Count;
                for (int i = count; i > positionTypes.Count; i--)
                {
                    castLocations.RemoveAt(count - 1);
                }
            }
        }
    }
}