using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HitboxSystem
{
    public class HitboxManager : MonoBehaviour
    {

        private Collider[] hitboxes;
        private float distanceFromFront = 0;

        public void Awake()
        {
            hitboxes = transform.GetComponents<Collider>();
            CalculateDistanceFromFront();
        }

        /// <summary>
        /// Finds the nearest point from the character in the direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Vector3 GetNearestPointFromDirection(Vector3 direction) {
            Vector3 position = transform.position + direction*1000;
            Vector3 result = transform.position;
            float distance = 0;

            for (int i = 0; i < hitboxes.Length; i++) {
                Vector3 nearestPoint = hitboxes[0].ClosestPoint(position);
                float distanceFromPoint = (nearestPoint - transform.position).magnitude;

                if (distanceFromPoint > distance) {
                    result = nearestPoint;
                    distance = distanceFromPoint;
                }
            }

            return result;
        }


        /// <summary>
        /// Gets the nearest point from the position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector3 GetNearestPointFromPosition(Vector3 position) {
            Vector3 result = transform.position;
            float distance = 99999;

            for (int i = 0; i < hitboxes.Length; i++) {
                Vector3 nearestPos = hitboxes[i].ClosestPoint(position);
                float nearestDistance = (nearestPos - position).magnitude;

                if (nearestDistance < distance) {
                    result = nearestPos;
                    distance = nearestDistance;
                }
            }

            return result;
        }


        private void CalculateDistanceFromFront() {
            Vector3 frontVector = GetNearestPointFromDirection(transform.forward);
            distanceFromFront = (frontVector - transform.position).magnitude;
        }

        public float GetDistanceFromFront() {
            return distanceFromFront;
        }

    }
}