using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTargetter : MonoBehaviour {

    private List<TargetPoint> allFriendlyTargets = new List<TargetPoint>();
    [SerializeField] private List<TargetPoint> allEnemyTargets = new List<TargetPoint>();
    private SphereCollider targetCollider;
    private CharacterManager manager;

    public TargetPoint target;
    private int index;

    public int colliderSize {
        get {
            return _colliderSize;
        }
        set {
            _colliderSize = value;
            ScaleCollider(value);
        }
    }
    [SerializeField] private int _colliderSize;



    // Use this for initialization
    void Start() {
        initCollider();
        manager = transform.GetComponent<CharacterManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    //PUBLIC FUNCTIONS

    public TargetPoint GetNearestTarget() {
        TargetPoint result = null;
        float nearest = 999;

        for (int i = 0; i < allEnemyTargets.Count; i++) {
            TargetPoint currentTarget = allEnemyTargets[i];
            float distance = DistanceFromTarget(currentTarget);

            if (distance < nearest) {
                result = currentTarget;
                nearest = distance;
                index = i;
            }
        }

        return result;
    }

    //increment or decrement the target
    public TargetPoint SwitchTarget(bool reverse = false) {
        int totalTargets = allEnemyTargets.Count;
        TargetPoint result = null;

        if (totalTargets != 0) {
            if (!reverse)
            {
                index = (index + 1) % totalTargets;
            }
            else {
                index = (index + totalTargets - 1) % totalTargets;
            }

            result = allEnemyTargets[index];
        }

        return result;
    }





    //PRIVATE FUNCTIONS

    private float DistanceFromTarget(TargetPoint target) {
        return (transform.position - target.transform.position).magnitude;
    }
    

    //Adds an enemy to the list when entering the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy") {
            TargetPoint target = other.GetComponent<TargetPoint>();

            if (target != null) {
                allEnemyTargets.Add(target);
            }
        }
    }

    //Removes an enemy from the list when exiting the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy") {
            TargetPoint target = other.GetComponent<TargetPoint>();

            if (target != null)
            {
                allEnemyTargets.Remove(target);
            }
        }
    }


    private void initCollider() {
        targetCollider = transform.GetComponent<SphereCollider>();
        ScaleCollider(_colliderSize);
    }

    private void ScaleCollider(int scale) {
        targetCollider.radius = scale;
    }
}
