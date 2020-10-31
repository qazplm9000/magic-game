using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargettingSystem
{
    public class PlayerTargetter : MonoBehaviour, ITargetter
    {
        public TargetTracker tracker;
        private Combatant character;
        public KeyCode targetButton = KeyCode.Tab;
        public Combatant currentTarget = null;
        public Camera cam;
        private Vector3 screenCenter;

        // Start is called before the first frame update
        void Start()
        {
            character = transform.GetComponent<Combatant>();
            cam = Camera.main;
            screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            if(tracker == null)
            {
                tracker = Instantiate(WorldManager.GetTrackerPrefab());
                tracker.transform.position = character.transform.position;
                tracker.transform.SetParent(character.transform);
                tracker.InitTracker(character);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(targetButton) && currentTarget == null)
            {
                currentTarget = GetTargetClosestToScreen();
            }else if (Input.GetKeyDown(targetButton))
            {
                currentTarget = GetTargetClosestToLeft(currentTarget);
            }


            if(currentTarget != null)
            {
                Debug.DrawRay(currentTarget.transform.position, transform.up * 10, Color.blue);

                if (currentTarget.IsDead())
                {
                    currentTarget = null;
                }
            }
        }


        /* ITargetter functions */

        public Combatant TargetEnemy(bool targetNext = true)
        {
            Combatant currentTarget = character.GetCurrentTarget();
            Combatant result = null;

            if(currentTarget == null)
            {
                result = GetTargetClosestToScreen();
            }
            else if(targetNext)
            {
                result = GetTargetClosestToRight(result);
            }
            else
            {
                result = GetTargetClosestToLeft(result);
            }

            currentTarget = result;
            return result;
        }

        public Combatant TargetAlly(bool targetNext = true)
        {
            return null;
        }


        public Combatant GetCurrentTarget()
        {
            return currentTarget;
        }

        public void Untarget()
        {
            currentTarget = null;
        }








        /* Helper functions */


        private Combatant GetTargetClosestToScreen()
        {
            Combatant result = null;
            float dist = float.PositiveInfinity;

            List<Combatant> targets = tracker.targets;
            for(int i = 0; i < targets.Count; i++)
            {
                Combatant temp = targets[i];

                if (temp.IsDead())
                {
                    continue;
                }

                Vector3 screenPos = cam.WorldToScreenPoint(temp.transform.position);
                float tempDist = DistanceFromCenterOfScreen(screenPos);
                //Debug.Log($"{temp.name} - {tempDist} - {screenPos}");

                if(tempDist < dist)
                {
                    dist = tempDist;
                    result = temp;
                }
            }

            return result;
        }

        private Combatant GetTargetClosestToRight(Combatant currentTarget)
        {
            Combatant result = null;
            float angleBetween = float.PositiveInfinity;

            List<Combatant> targets = tracker.targets;
            for (int i = 0; i < targets.Count; i++) {
                if (targets[i] != currentTarget && !targets[i].IsDead())
                {
                    float tempAngle = AngleBetweenTarget(targets[i]);
                    tempAngle = tempAngle > 0 ? tempAngle : 360 + tempAngle;

                    if(tempAngle < angleBetween)
                    {
                        result = targets[i];
                        angleBetween = tempAngle;
                    }
                }
            }

            return result;
        }


        private Combatant GetTargetClosestToLeft(Combatant currentTarget)
        {
            Combatant result = null;
            float angleBetween = float.PositiveInfinity;

            List<Combatant> targets = tracker.targets;
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] != currentTarget && !targets[i].IsDead())
                {
                    float tempAngle = AngleBetweenTarget(targets[i]);
                    tempAngle = tempAngle > 0 ? 360 - tempAngle : -tempAngle;

                    if (tempAngle < angleBetween)
                    {
                        result = targets[i];
                        angleBetween = tempAngle;
                    }
                }
            }

            return result;
        }


        private float DistanceFromCenterOfScreen(Vector3 screenPos)
        {
            return (screenPos - screenCenter).magnitude;
        }

        private float AngleBetweenTarget(Combatant target)
        {
            Vector3 directionVec = currentTarget.transform.position - transform.position;
            Vector3 targetVec = target.transform.position - transform.position;
            return Vector3.SignedAngle(directionVec, targetVec, transform.up);
        }

    }
}