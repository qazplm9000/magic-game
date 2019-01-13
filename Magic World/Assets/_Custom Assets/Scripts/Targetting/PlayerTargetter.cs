using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTargetter : MonoBehaviour {

    private List<CharacterManager> allFriendlyTargets = new List<CharacterManager>();
    [SerializeField] private List<CharacterManager> allEnemyTargets = new List<CharacterManager>();
    private SphereCollider targetCollider;
    private CharacterManager manager;

    public CharacterManager target;
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
        //initCollider();
        manager = transform.GetComponent<CharacterManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    //PUBLIC FUNCTIONS

    public CharacterManager GetNearestTarget() {
        CharacterManager result = null;
        float nearest = 999;

        for (int i = 0; i < allEnemyTargets.Count; i++) {
            CharacterManager currentTarget = allEnemyTargets[i];
            float distance = DistanceFromTarget(currentTarget);

            if (distance < nearest) {
                result = currentTarget;
                nearest = distance;
                index = i;
            }
        }

        return result;
    }


    public TargetPoint GetNearestTargetPoint() {
        TargetPoint result = null;
        float distance = float.MaxValue;

        TargetPoint[] targets = FindObjectsOfType<TargetPoint>();

        for (int i = 0; i < targets.Length; i++) {
            //eventually should check if target is an enemy instead
            if (targets[i].manager != manager) {
                float newDistance = (targets[i].transform.position - transform.position).magnitude;
                if (distance > newDistance) {
                    distance = newDistance;
                    result = targets[i];
                }
            }
        }

        return result;
    }



    //increment or decrement the target
    public CharacterManager SwitchTarget(bool reverse = false) {
        int totalTargets = allEnemyTargets.Count;
        CharacterManager result = null;

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

    private float DistanceFromTarget(CharacterManager target) {
        return (transform.position - target.transform.position).magnitude;
    }
    

    //Adds an enemy to the list when entering the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy") {
            CharacterManager target = other.GetComponent<CharacterManager>();

            if (target != null) {
                allEnemyTargets.Add(target);
            }
        }
    }

    //Removes an enemy from the list when exiting the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy") {
            CharacterManager target = other.GetComponent<CharacterManager>();

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
