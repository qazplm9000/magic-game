using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace TargettingSystem
{
    public class PlayerTargetter : MonoBehaviour, ITargetter
    {
        private ITargettable player;
        //public TargetTracker tracker;
        private ITargettable currentTarget
        {   
            get 
            { 
                return _currentTarget; 
            } 
            set 
            { 
                _currentTarget = value; 
                onTargetChanged.Invoke(value); 
            } 
        }
        private ITargettable _currentTarget;

        private Camera cam;
        private Vector3 screenCenter;

        [SerializeField]
        private GameObject targetReference;

        [SerializeField]
        private float targetterRange = 10f;


        private UnityEvent<ITargettable> onTargetChanged;


        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
            screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            player = GetComponent<ITargettable>();
            onTargetChanged = new UnityEvent<ITargettable>();
            onTargetChanged.AddListener(SetTargetObject);
        }

        // Update is called once per frame
        void Update()
        {
            if(currentTarget != null)
            {
                var currentTarPos = currentTarget.GetTransform().position;
                Debug.DrawRay(currentTarPos, transform.up * 10, Color.blue);

                if (currentTarget.IsDead())
                {
                    currentTarget = null;
                }
            }
        }


        /* ITargetter functions */

        public ITargettable TargetEnemy(bool targetNext = true)
        {
            ITargettable result = null;

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

        public ITargettable TargetAlly(bool targetNext = true)
        {
            return null;
        }


        public ITargettable GetCurrentTarget()
        {
            return currentTarget;
        }

        public void Untarget()
        {
            currentTarget = null;
        }








        /* Helper functions */


        private ITargettable GetTargetClosestToScreen()
        {
            ITargettable result = null;
            float dist = float.PositiveInfinity;
            var targets = GetAllTargetsInRange(targetterRange);

            for(int i = 0; i < targets.Count; i++)
            {
                ITargettable tempTarget = targets[i];

                if (tempTarget.IsDead())
                {
                    continue;
                }

                Vector3 screenPos = cam.WorldToScreenPoint(tempTarget.GetTransform().position);
                float tempDist = DistanceFromCenterOfScreen(screenPos);
                //Debug.Log($"{temp.name} - {tempDist} - {screenPos}");

                if(tempDist < dist)
                {
                    dist = tempDist;
                    result = tempTarget;
                }
            }

            return result;
        }

        private ITargettable GetTargetClosestToRight(ITargettable currentTarget)
        {
            ITargettable result = null;
            float angleBetween = float.PositiveInfinity;

            var targets = GetAllTargetsInRange(targetterRange);

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


        private ITargettable GetTargetClosestToLeft(ITargettable currentTarget)
        {
            ITargettable result = null;
            float angleBetween = float.PositiveInfinity;

            var targets = GetAllTargetsInRange(targetterRange);

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



        private List<ITargettable> GetAllTargetsInRange(float range)
        {
            var allTargets = FindObjectsOfType<MonoBehaviour>()
                .OfType<ITargettable>()
                .Where(x =>
                        UnityUtilities.GetVectorBetween(
                            x.GetTransform().position,
                            transform.position)
                        .magnitude < range)
                .Where(x => x != player);
            List<ITargettable> targets = new List<ITargettable>();
            targets.AddRange(allTargets);

            return targets;
        }


        private float DistanceFromCenterOfScreen(Vector3 screenPos)
        {
            return (screenPos - screenCenter).magnitude;
        }

        private float AngleBetweenTarget(ITargettable target)
        {
            Vector3 directionVec = currentTarget.GetTransform().position - transform.position;
            Vector3 targetVec = target.GetTransform().position - transform.position;
            return Vector3.SignedAngle(directionVec, targetVec, transform.up);
        }

        private void SetTargetObject(ITargettable target)
        {
            if(target != null)
            {
                targetReference = target.GetGameObject();
            }
            else
            {
                targetReference = null;
            }
        }



        //Event subscriptions

        public void SubscribeToOnTargetChanged(UnityAction<ITargettable> action)
        {
            onTargetChanged.AddListener(action);
        }
        public void ClearAllListeners()
        {
            onTargetChanged.RemoveAllListeners();
        }
    }
}