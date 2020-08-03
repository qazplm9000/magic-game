using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Combatant target;
    public GameObject focusPoint;
    
    public float offsetHeight = 2;
    public float minDistance = 2;
    public float maxDistance = 5;
    public float speed = 5;
    [Range(0,1)]
    public float lerpFactor = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowTargetHorizontally();
        SetCameraHeight();
        LookAtTarget();
        
    }
    


    private void FollowTargetHorizontally()
    {
        Vector3 distanceVec = focusPoint.transform.position - transform.position;
        float distance = distanceVec.magnitude;
        if(distance > maxDistance)
        {
            distanceVec.y = 0;
            distanceVec.Normalize();
            float minDistance = distance - maxDistance;
            Vector3 movementVec = distanceVec * speed * Time.deltaTime;
            float movementDist = movementVec.magnitude;
            if(minDistance < movementDist)
            {
                movementVec = distanceVec * minDistance;
            }
            transform.position = Vector3.Lerp(transform.position, 
                                        transform.position + movementVec,
                                        lerpFactor);
        }
    }

    private void SetCameraHeight()
    {
        transform.position = new Vector3(transform.position.x, focusPoint.transform.position.y + offsetHeight, transform.position.z);
    }

    private void LookAtTarget()
    {
        Combatant targetsTarget = target.GetTarget();
        if (targetsTarget != null)
        {
            transform.LookAt(targetsTarget.transform);
        }
        else
        {
            transform.LookAt(focusPoint.transform);
        }
    }
}
